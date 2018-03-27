using Microsoft.ClearScript.V8;
using Snail.Collector.Common;
using Snail.Collector.Core.Configuration;
using Snail.Collector.Core.SystemModules;
using Snail.Collector.Http;
using SnailCore.Data;
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
        private const string LogSource = "task";

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
        internal event EventHandler<TaskEventArgs> OnStart;

        /// <summary>
        /// 在任务结束时触发
        /// </summary>
        internal event EventHandler<TaskEventArgs> OnStop;

        /// <summary>
        /// 在任务发生异常时触发
        /// </summary>
        internal event EventHandler<TaskErrorEventArgs> OnError;

        /// <summary>
        /// 任务执行统计信息
        /// </summary>
        public TaskStatictics Stat { get; private set; }

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
        /// <param name="isTest">是否为测试任务</param>
        public Task(string cfgFile)
        {           
            this.ExecutePath = FileUnity.GetDrectory(cfgFile);            
            this._freeSE = new Queue<TaskInvoker>();
            var script = FileUnity.ReadConfigFile(cfgFile);                     
            if (string.IsNullOrEmpty(script))
            {
                throw new Exception("task init error, the config file content is empty.");
            }
            this.TaskSetting = Serializer.JsonDeserialize<TaskSetting>(script);
            this.InitTaskStorage();
            this._lock = new Semaphore(this.TaskSetting.Parallel, this.TaskSetting.Parallel);
        }

        internal Task() { }
       
        /// <summary>
        /// 初始化任务存储
        /// </summary>
        private void InitTaskStorage()
        {
            if (!TaskItems.Instance.AddRoot(new TaskItemEntity()
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
        internal bool Run()
        {
            Init();
            if (Status != TaskStatus.Init)
            {
                return false;
            }
            lock (LockObj)
            {
                if (Status != TaskStatus.Init)
                {
                    return false;
                }
                Status = TaskStatus.Running;
            }

            new Thread(() =>
            {
                try
                {                   
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
                                OnStop?.Invoke(this, new TaskEventArgs() { Task = this });
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
                }
                catch (Exception ex)
                {
                    LoggerProxy.Error(LogSource, "call Run error.", ex);
                    OnError?.Invoke(this, new TaskErrorEventArgs()
                    {
                        Ex = ex,
                        Task = this
                    });              
                }                
            }).Start();
            OnStart?.Invoke(this, new TaskEventArgs() { Task = this });
            return true;
        }

        /// <summary>
        /// 结束任务
        /// </summary>
        internal bool Stop()
        {
            if (this.Status != TaskStatus.Running)
            {
                return false;
            }
            lock (this)
            {
                if (this.Status != TaskStatus.Running)
                {
                    return false;
                }
                this.Status = TaskStatus.Stopping;
            }
            return true;
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
        /// 用于执行js代理脚本
        /// </summary>
        private dynamic _script;

        private object LockObj = new object();      

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
                this.Stat = new TaskStatictics() { StartTime = DateTime.Now };
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
                this.Stat.EndTime = DateTime.Now;
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
                        LoggerProxy.Error(LogSource, "call Free error.", ex);
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
            try
            {
                var item = TaskItems.Instance.GetExec(this.TaskId);
                if (item == null)
                {
                    return null;
                }
                var settings = new TaskItemSetting()
                {
                    ScriptFile = System.IO.Path.Combine(this.ExecutePath, item.Script),
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
                        invoker = new TaskInvoker(this);
                        this._count++;
                    }
                }
                invoker.SetSetting(settings);
                Interlocked.Increment(ref this._busyCount);
                return invoker;
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, "call NewInvoker failed.", ex);
            }
            return null;            
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
                        if (!TaskItems.Instance.Update(invoker.Result.TaskInvokerInfo))
                        {
                            LoggerProxy.Error(LogSource, "update callInvoker status failed.");
                        }
                        // 新增统计信息
                        this.SetStat(1, invoker.Result.Success ? TaskStatTypes.Task : TaskStatTypes.ErrTask);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, "call FreeInvoker failed.", ex);                
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

        /// <summary>
        /// 设置统计信息
        /// </summary>
        /// <param name="num">数量值</param>
        /// <param name="type">统计类型</param>
        public void SetStat(int num, TaskStatTypes type)
        {
            lock (this)
            {
                switch (type)
                {
                    case TaskStatTypes.NewTask:
                        this.Stat.NewTaskCount++;
                        break;
                    case TaskStatTypes.Task:
                        this.Stat.ExecTaskCount++;
                        break;
                    case TaskStatTypes.File:
                        this.Stat.FileCount++;
                        break;
                    case TaskStatTypes.Article:
                        this.Stat.ArticleCount++;
                        break;
                    case TaskStatTypes.ErrTask:
                        this.Stat.ErrTaskCount++;
                        break;
                }
            }
        }

        #endregion
    }
}
