using Microsoft.ClearScript.V8;
using Snail.Collector.Core.Configuration;
using Snail.Collector.Http;
using Snail.Data;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 封装一个采集任务
    /// </summary>
    public sealed class Task
    {
        /// <summary>
        /// 获取任务Id
        /// </summary>
        public int TaskId { get { return this.TaskSetting.TaskId; } }

        /// <summary>
        /// 获取任务名称
        /// </summary>
        public string TaskName { get { return this.TaskSetting.TaskName; } }

        /// <summary>
        /// 获取任务执行的主目录
        /// </summary>
        public string ExecutePath { get; private set; }

        /// <summary>
        /// 获取任务配置文件路径
        /// </summary>
        public string ConfigFile { get; private set; }

        /// <summary>
        /// 获取当前执行环境的状态
        /// </summary>
        public TaskStatus Status { get; private set; }

        /// <summary>
        /// 在任务开始执行时触发
        /// </summary>
        internal event EventHandler OnStart;

        /// <summary>
        /// 在任务结束时触发
        /// </summary>
        internal event EventHandler OnStop;

        /// <summary>
        /// 获取当前上下文
        /// </summary>
        public TaskContext Context { get; private set; }

        private int _count;
        /// <summary>
        /// 获取当前实例化的任务执行者数
        /// </summary>
        public int InvokerCount { get { return this._count; } }

        /// <summary>
        /// 获取当前空闲的任务执行者数量
        /// </summary>
        public int InvokerFreeCount
        {
            get
            {
                lock (this)
                {
                    return this._freeSE.Count;
                }
            }
        }

        private int _busyCount;
        /// <summary>
        /// 获取正在执行任务的任务数
        /// </summary>
        public int InvokerBusyCount { get { return this._busyCount; } }

        /// <summary>
        /// 获取任务配置信息
        /// </summary>
        public TaskSetting TaskSetting { get; private set; }

        /// <summary>
        /// 初始化任务生成工厂
        /// </summary>        
        /// <param name="cfgFile">任务工厂配置文件</param>
        public Task(string cfgFile)
        {
            this.ExecutePath = FileUnity.GetDrectory(cfgFile);
            this.ConfigFile = cfgFile;
            this._mainSE = new V8ScriptEngine();
            this._mainSE.LoadSystemModules();
            this._freeSE = new Queue<TaskInvoker>();
            this._http = new HttpModule();
            this._mainSE.AddHostObject("http", this._http);
            // 临时绑定上下文到当前线程中，该引用会被下一个初始化任务覆盖
            this.Context = new TaskContext();
            this.Context.ExecutePath = this.ExecutePath;
            this.Context.HttpClient = this._http;
            this.Context.Engine = this._mainSE;
            this.Context.BindContext();
            this.ExecuteInitScript();         
            this.Context.TaskId = this.TaskId;
            this._lock = new Semaphore(this.TaskSetting.Parallel, this.TaskSetting.Parallel);
        }

        /// <summary>
        /// 执行初始化脚本
        /// </summary>
        private void ExecuteInitScript()
        {
            var script = FileUnity.ReadConfigFile(this.ConfigFile);
            this._mainSE.Execute(script);
            this._script = this._mainSE.Invoke("config");
            if (this._script == null)
            {
                throw new Exception("Task Init Error, the \"config\" method has no return value.");
            }
            var strSet = Serializer.JsonSerialize(this._script);
            if (string.IsNullOrWhiteSpace(strSet))
            {
                throw new Exception("Task Init Error, the \"config\" method return value is empty.");
            }
            this.TaskSetting = Serializer.JsonDeserialize<TaskSetting>(strSet);
            if (!TaskInvokerStorage.Instance.AddRoot(new TaskInvokerStorageEntity()
            {
                ParentId = 0,
                Script = this.TaskSetting.Script,
                TaskId = this.TaskId,
                Url = this.TaskSetting.Url
            }))
            {
                throw new Exception("Task Init Error, add rootTask failed.");
            }            
        }

        /// <summary>
        /// 执行任务
        /// </summary>        
        internal void Run()
        {
            Init();

            if (Status != TaskStatus.Init)
            {
                return;
            }
            lock (LockObj)
            {
                if (Status != TaskStatus.Init)
                {
                    return;
                }
                Status = TaskStatus.Running;
            }

            new Thread(() =>
            {
                // 绑定任务执行上下文
                this.Context.BindContext();

                while (true)
                {
                    var isStop = false;
                    lock (this)
                    {
                        isStop = this.Status == TaskStatus.Stopping;
                    }
                    // 获取执行者
                    TaskInvoker invoker = isStop ? null : NewInvoker();
                    if (invoker == null)
                    {                       
                        if (SetStop())
                        {
                            OnStop?.Invoke(this, null);
                            this.Free();
                            break;
                        }
                        Thread.Sleep(1000);
                        continue;
                    }
                    invoker.RunAsync((task) =>
                    {                       
                        // 释放执行者
                        FreeInvoker(task);                        
                    });
                }
            }).Start();

            OnStart?.Invoke(this, null);
        }

        /// <summary>
        /// 结束任务
        /// </summary>
        internal void Stop()
        {
            if (this.Status != TaskStatus.Running)
            {
                return;
            }
            lock (this)
            {
                if (this.Status != TaskStatus.Running)
                {
                    return;
                }
                this.Status = TaskStatus.Stopping;
            }
        }

        #region 私有成员

        /// <summary>
        /// 协调多线程访问
        /// </summary>
        private Semaphore _lock;

        /// <summary>
        /// 记录空闲的任务执行者
        /// </summary>
        private Queue<TaskInvoker> _freeSE;

        /// <summary>
        /// 记录任务生成器运行的V8实例
        /// </summary>
        private V8ScriptEngine _mainSE;

        /// <summary>
        /// 用于执行js代理脚本
        /// </summary>
        private dynamic _script;

        private object LockObj = new object();

        private HttpModule _http;

        #endregion

        #region 私有方法     
        /// <summary>
        /// 重新初始化任务，清空任务上下文
        /// </summary>
        private void Init()
        {
            if (Status != TaskStatus.None && Status != TaskStatus.Stop)
            {
                return;
            }
            lock (LockObj)
            {
                if (Status != TaskStatus.None && Status != TaskStatus.Stop)
                {
                    return;
                }
                Status = TaskStatus.Init;
            }
        }

        private bool SetStop()
        {
            if (InvokerBusyCount <= 0)
            {
                lock (LockObj)
                {
                    Status = TaskStatus.Stop;
                }                
                return true;
            }
            return false;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        private void Free()
        {
            // 释放任务执行者
            if (this._freeSE.Count > 0)
            {
                do
                {
                    try
                    {
                        if (this._freeSE.Count <= 0)
                        {
                            break;
                        }
                        var item = this._freeSE.Dequeue();                      
                        if (item == null)
                        {
                            break;
                        }
                        item.Dispose();
                    }
                    catch (Exception ex)
                    {
                        // todo: 日志
                    }
                }
                while (true);
            }
        }

        /// <summary>
        /// 获取一个新的采集任务，如果当前繁忙，此方法将会等待
        /// </summary>        
        /// <returns></returns>
        private TaskInvoker NewInvoker()
        {
            var item = TaskInvokerStorage.Instance.GetExec(this.TaskId);
            if (item == null)
            {
                return null;
            }
            var settings = new TaskItemSetting()
            {
                ScriptFile = System.IO.Path.Combine(this.Context.ExecutePath, item.Script),
                Url = item.Url,
                TaskInvokerInfo = item              
            };            
            this._lock.WaitOne();
            TaskInvoker invoker = null;
            lock (this)
            {
                if (this._freeSE.Count > 0)
                {
                    invoker = this._freeSE.Dequeue();
                }
                if (invoker == null)
                {
                    invoker = new TaskInvoker(this.Context);
                    this._count++;
                }
            }
            invoker.SetSetting(settings);
            Interlocked.Increment(ref this._busyCount);
            return invoker;
        }

        /// <summary>
        /// 释放一个任务执行者
        /// </summary>
        /// <param name="invoker"></param>
        private void FreeInvoker(TaskInvoker invoker)
        {
            try
            {
                if (invoker.Result != null)
                {
                    // 设置任务状态
                    if (invoker.Result.TaskInvokerInfo != null)
                    {
                        invoker.Result.TaskInvokerInfo.Status = invoker.Result.Success ? 3 :
                            invoker.Result.TaskInvokerInfo.ExecCount >= ConfigManager.Current.ErrorRetry ? 4 : 2;
                        if (!TaskInvokerStorage.Instance.Update(invoker.Result.TaskInvokerInfo))
                        {
                            // todo: 记录日志
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // todo: 写入日志
            }
            finally
            {
                try
                {
                    Interlocked.Decrement(ref this._busyCount);
                    lock (this)
                    {
                        this._freeSE.Enqueue(invoker);

                        this._lock.Release();
                    }
                }
                catch { }
            }            
        }
      
        #endregion
    }
}
