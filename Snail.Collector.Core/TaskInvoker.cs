using Microsoft.ClearScript.V8;
using Snail.Collector.Common;
using System;
using System.Threading;
using NetTask = System.Threading.Tasks.Task;
using NetTaskFactory = System.Threading.Tasks.TaskFactory;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 采集任务执行者
    /// </summary>
    public class TaskInvoker:IDisposable
    {
        private const string LogSource = "taskinvoker";

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
        /// <param name="task"></param>
        public TaskInvoker(Task task)
        {
            this.Status = TaskInvokerStatus.Init;
            this.Context = new TaskInvokerContext();
            this.Context.Task = task;
            this.Context.ExecutePath = task.ExecutePath;
            this._innerSE = new V8ScriptEngine();
            this._innerSE.LoadSystemModules();                      
            this.Context.Engine = this._innerSE;
            this._worker = new Thread(ThreadWork);
            this._notify = new AutoResetEvent(false);
            this._worker.Start();
        }

        public TaskInvoker(string scriptFile)
        {
            this._innerSE = new V8ScriptEngine();
            this._innerSE.LoadSystemModules();
            this.Context = new TaskInvokerContext();
            this.Context.ExecutePath = FileUnity.GetDrectory(scriptFile);
            this.Context.Engine = this._innerSE;
            this.BindContext();
            this._needInit = true;
            this.InitScript(scriptFile);
        }

        public bool Run()
        {
            return this.ExecItem("");
        }


        ///// <summary>
        ///// 执行一次测试任务
        ///// </summary>
        ///// <param name="scriptFile"></param>
        ///// <param name="onStart"></param>
        //public static bool Run(string scriptFile, Action<TaskInvoker> onStart = null)
        //{
        //    using (var invoker = new TaskInvoker())
        //    {               
        //        invoker._innerSE = new V8ScriptEngine();
        //        invoker._innerSE.LoadSystemModules();
        //        invoker.Context = new TaskInvokerContext();
        //        invoker.Context.ExecutePath = FileUnity.GetDrectory(scriptFile);
        //        invoker.Context.Engine = invoker._innerSE;
        //        invoker.BindContext();
        //        invoker._needInit = true;
        //        onStart?.Invoke(invoker);
        //        if (!invoker.InitScript(scriptFile))
        //        {
        //            return false;
        //        }
        //        return invoker.ExecItem("");
        //    }
        //}

        //public static NetTask RunAsync(string scriptFile, Action<TaskInvoker> onStart = null)
        //{
        //    return new NetTaskFactory().StartNew(objData =>
        //    {
        //        var data = objData as Tuple<string, Action<TaskInvoker>>;
        //        return Run(data.Item1, data.Item2);
        //    }, new Tuple<string, Action<TaskInvoker>>(scriptFile, onStart));
        //}

        /// <summary>
        /// 添加宿主对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        public void AddHostObj(string name, object obj)
        {
            this._innerSE.AddHostObject(name, obj);
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
            this.BindContext();
            do
            {
                this._notify.WaitOne();
                if (!this.InitScript(this.CurrSetting.ScriptFile))
                {
                    continue;
                }
                this.Result.Success = ExecItem(this.CurrSetting.Url);
                this.Status = TaskInvokerStatus.Stop;
                try
                {
                    this._callBack?.Invoke(this);
                }
                catch (Exception ex)
                {
                    LoggerProxy.Error(LogSource, string.Format("call ThreadWork-item callback error,url:'{0}'.", this.CurrSetting.Url), ex);
                }
            }
            while (true);
        }

        /// <summary>
        /// 初始化脚本
        /// </summary>
        /// <param name="scriptFile"></param>
        private bool InitScript(string scriptFile)
        {
            try
            {
                if (this._needInit)
                {
                    // 初始化执行脚本                        
                    this._innerSE.Execute(FileUnity.ReadConfigFile(scriptFile));
                    this._innerSE.Execute(@"function ______tryParse(uri){ 
try{
    var result = parse(uri);
    if(result == undefined || result == 1 || result == true){
        return 'OK';
    }
    else{
        return 'parse is not return 1 or true';
    }     
}catch(e){
    return e.message;
}}");
                    this._needInit = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, string.Format("call InitScript error,scriptFile:'{0}'.{1}.", scriptFile, ex.Message), ex);
            }
            return false;
        }

        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private bool ExecItem(string uri)
        {
            try
            {                
                var execRest = this._innerSE.Invoke("______tryParse", uri).ToString();
                if (execRest != "OK")
                {
                    throw new Exception(execRest);
                }
                return true;
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, string.Format("call ExecItem error,url:'{0}'.{1}.", uri, ex.Message), ex);
            }
            return false;
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
                this._worker?.Abort();
            }
            catch { }
            if (disposing)
            {
                this._innerSE?.Dispose();
                this._notify?.Dispose();
            }            
        }
        #endregion

    }
}
