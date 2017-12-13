using Microsoft.ClearScript.V8;
using Snail.Collector.Core.SystemModules;
using Snail.Collector.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 采集任务执行者
    /// </summary>
    internal class TaskInvoker:IDisposable
    {
        /// <summary>
        /// 任务执行状态
        /// </summary>
        public TaskInvokerStatus Status { get; private set; }

        /// <summary>
        /// 在任务执行后触发
        /// </summary>
        private Action<TaskInvoker> _callBack;

        /// <summary>
        /// 获取或设置此次任务执行的结果
        /// </summary>
        public TaskInvokerExecResult Result { get; set; }

        /// <summary>
        /// 工作线程类
        /// </summary>
        private Thread _worker;

        /// <summary>
        /// 任务执行通知
        /// </summary>
        private AutoResetEvent _notify;

        /// <summary>
        /// 获取当前任务执行的配置
        /// </summary>
        public TaskItemSetting CurrSetting { get; private set; }
         
        /// <summary>
        /// 获取执行上下文
        /// </summary>
        public TaskInvokerContext Context { get; set; }

        private V8ScriptEngine _innerSE;

        private bool _needInit = false;

        private bool _boundContxt = false;

        /// <summary>
        /// 初始化一个任务执行者
        /// </summary>       
        /// <param name="http"></param>
        public TaskInvoker(TaskContext taskContext)
        {
            this.Status = TaskInvokerStatus.Init;
            this.Context = new TaskInvokerContext();
            this.Context.TaskContext = taskContext;
            this._innerSE = new V8ScriptEngine();
            this._innerSE.LoadSystemModules();
            this._innerSE.AddHostObject("http", this.Context.TaskContext.HttpClient);
            this._innerSE.AddHostObject("storage", new StorageDataModuleExtend(taskContext.Settings.Storage));
            var storageProxy = Unity.ReadResource("Snail.Collector.Storage.StorageDataModule.js", Assembly.GetAssembly(typeof(StorageDataModule)));
            if (!string.IsNullOrEmpty(storageProxy))
            {
                this._innerSE.Execute(storageProxy);
            }
            this.Context.Engine = this._innerSE;
            this._worker = new Thread(ThreadWork);
            this._notify = new AutoResetEvent(false);
            this._worker.Start();
        }

        /// <summary>
        /// 设置当前的配置信息
        /// </summary>
        /// <param name="set"></param>
        public void SetSetting(TaskItemSetting set)
        {
            lock (this)
            {
                if (this.Status == TaskInvokerStatus.Running)
                {
                    throw new Exception("invoker is busy.");
                }
                this.Status = TaskInvokerStatus.Init;
            }
            // 重新初始化执行脚本
            if (this.CurrSetting == null || this.CurrSetting.ScriptFile != set.ScriptFile)
            {
                this._needInit = true;
            }
            this.CurrSetting = set;
            this.Context.TaskInvokerInfo = set.TaskInvokerInfo;
            this.Result = new TaskInvokerExecResult() { TaskInvokerInfo = set.TaskInvokerInfo };
        }

        /// <summary>
        /// 执行一次任务
        /// </summary>        
        public void RunAsync(Action<TaskInvoker> callBack)
        {
            lock (this)
            {
                // 这里的判断只作为调试线程同步，可以去掉
                if (this.Status != TaskInvokerStatus.Init)
                {
                    throw new Exception("invoker is busy or uninitialized.");
                }
                this.Status = TaskInvokerStatus.Running;
            }
            this._callBack = callBack;
            this._notify.Set();
        }

        /// <summary>
        /// 工作线程执行方法
        /// </summary>
        private void ThreadWork()
        {           
            // 绑定Invoker执行上下文
            this.BindContext();
            do
            {              
                this._notify.WaitOne();
                try
                {
                    if (this._needInit)
                    {
                        // 初始化执行脚本                        
                        this._innerSE.Execute(FileUnity.ReadConfigFile(this.CurrSetting.ScriptFile));
                        this._needInit = false;
                    }
                    // 执行任务
                    var execRest = this._innerSE.Invoke("parse", this.CurrSetting.Url);                   
                    if (int.TryParse(execRest == null ? "0" : execRest.ToString(), out int intRest) && intRest == 1)
                    {
                        this.Result.Success = true;
                    }
                    if (bool.TryParse(execRest == null ? "false" : execRest.ToString(), out bool boolRest) && boolRest)
                    {
                        this.Result.Success = true;
                    }
                }
                catch (Exception ex)
                {
                    // todo: 记录错误信息
                }
                finally
                {
                    this.Status = TaskInvokerStatus.Stop;
                    this._callBack?.Invoke(this);
                }
            }
            while (true);
        }

        /// <summary>
        /// 绑定任务执行上下文
        /// </summary>
        private void BindContext()
        {
            if (!this._boundContxt)
            {               
                ContextManager.SetTaskInvokerContext(this.Context);
                this._boundContxt = true;
            }
        }

        #region 资源释放

        private bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            try
            {
                this._worker.Abort();
            }
            catch { }
            if (disposing)
            {
                this._innerSE.Dispose();
                this._notify.Dispose();
            }            
        }
        #endregion

    }
}
