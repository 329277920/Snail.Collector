using Snail.Collector.Model;
using System;
using System.IO;
using System.Text;
using Snail.Collector.Modules;
using Snail.Collector.Modules.Http;
using Snail.Collector.Common;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 定义一个采集任务执行者
    /// </summary>
    public class CollectTaskInvoker : IDisposable
    {
        /// <summary>
        /// 执行脚本引擎
        /// </summary>
        public V8ScriptEngine ScriptEngine { get; private set; }  
       
        /// <summary>
        /// 记录当前执行的脚本文件路径
        /// </summary>
        private string _scriptPath;

        public CollectTaskInvoker()
        {
            this.ScriptEngine = new V8ScriptEngine();
            AddHostObject("lib", new HostTypeCollection("mscorlib", "System.Core"));
            AddHostObject("debug", new DebugModule());
            AddHostObject("log", new LoggerModule());
            AddHostObject("http", new HttpModule());
            this.ScriptEngine.AddHostObject("task", TypeContainer.Resolve<CollectTaskAccessProxy>());
            this.ScriptEngine.AddHostType("Array", typeof(JSArray));
            this.ScriptEngine.Execute(ResourceManager.ReadResource("Snail.Collector.JsExtends.stringExtend.js"));
            this.ScriptEngine.Execute(ResourceManager.ReadResource("Snail.Collector.JsExtends.unity.js"));
            this.ScriptEngine.Execute(ResourceManager.ReadResource("Snail.Collector.JsExtends.objectExtend.js"));
        }

        public object Invoke(CollectInfo collectInfo, CollectTaskInfo taskInfo)
        {
            CallContextManager.SetCollectInfo(collectInfo);
            CallContextManager.SetCollectTaskInfo(taskInfo);
           
            var scriptPath = taskInfo != null ? taskInfo.ScriptFilePath : collectInfo.ScriptFilePath;
            if (this._scriptPath == null || this._scriptPath != scriptPath)
            {
                this._scriptPath = scriptPath;
                this.ScriptEngine.Execute(this.GetInvokeScript());
            }
            return this.ScriptEngine.Invoke("___func___");
        }

        private void AddHostObject(string name, object value)
        {
            this.ScriptEngine.AddHostObject(name, value);
            if (value is IInitModule)
            {
                ((IInitModule)value).Init(this.ScriptEngine);
            }
        }

        private string GetInvokeScript()
        {
            if (!File.Exists(this._scriptPath))
            {
                throw new Exception($"执行任务失败，未找到脚本文件:'{this._scriptPath}'");
            }
            var script = "";
            using (FileStream fs = new FileStream(this._scriptPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    script = sr.ReadToEnd();
                }
            }
            string func = "___func___";
            StringBuilder buffer = new StringBuilder();
            buffer.Append($"function {func}(){{ \r\n");
            // buffer.Append("try \r\n { \r\n");
            buffer.Append($"{script} \r\n }} \r\n");
            //buffer.Append("catch (err) { \r\n");
            //buffer.Append("debug.writeLine(err.message); \r\n ");
            //buffer.Append("log.error(err.message); \r\n } \r\n");
            //buffer.Append("}");
            return buffer.ToString();
        }

        #region 资源释放

        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (this.ScriptEngine != null)
            {
                this.ScriptEngine.Dispose();
            }
            disposed = true;
        }

        #endregion

    }
}
