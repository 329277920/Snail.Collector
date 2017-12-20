using Microsoft.ClearScript.V8;
using Snail.Collector.Common;
using System;
using NetTask = System.Threading.Tasks.Task;
using NetTaskFactory = System.Threading.Tasks.TaskFactory;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务测试类
    /// </summary>
    public class TaskTester : IDisposable
    {
        static TaskTester()
        {
            TaskErrorMananger.Instance.Init();
        }

        /// <summary>
        /// 获取执行上下文
        /// </summary>
        public TaskInvokerContext Context { get; set; }

        private V8ScriptEngine _innerSE;

        /// <summary>
        /// 初始化一个任务执行者
        /// </summary>              
        public TaskTester()
        {
            this.Context = new TaskInvokerContext();          
            this._innerSE = new V8ScriptEngine();
            this._innerSE.LoadSystemModules();
            this.Context.Engine = this._innerSE;
        }

        public void AddObj(string name, object obj)
        {
            this._innerSE.AddHostObject(name, obj);
        }

        /// <summary>
        /// 运行脚本
        /// </summary>
        /// <param name="script"></param>         
        public bool Run(string script)
        {
            ContextManager.SetTaskInvokerContext(this.Context);
            this._innerSE.Execute(script);
            this._innerSE.Execute(@"function ______tryParse(){ 
try{
    var result = parse();
    if(result == undefined || result == 1 || result == true){
        return 'OK';
    }
    else{
        return 'parse is not return 1 or true';
    }     
}catch(e){
    return e.message;
}}");
            // 执行任务
            var execRest = this._innerSE.Invoke("______tryParse");
            if (execRest.ToString() == "OK")
            {
                return true;
            }
            LoggerProxy.Error("taskTester", "执行任务失败," + execRest.ToString());
            return false;
        }

        public NetTask RunAsync(string script)
        {
            return new NetTaskFactory().StartNew(objData =>
            {
                var data = objData as Tuple<string, string>;
                return Run(objData.ToString());
            }, script);
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
            if (disposing)
            {
                this._innerSE.Dispose();           
            }
        }
        #endregion
    }    
}
