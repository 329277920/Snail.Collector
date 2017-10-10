using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 采集器
    /// </summary>
    public class Collector
    {
        /// <summary>
        /// 获取当前用于采集执行的 V8ScriptEngine
        /// </summary>
        public V8ScriptEngine ScriptEngine { get; internal set; }

        /// <summary>
        /// 获取或设置该采集器的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取任务执行的脚本
        /// </summary>
        public string Code { get; internal set; }

        internal Collector(string name, string code)
        {
            Name = name;
            Code = code;
            _lock = new Semaphore(0, 1);
            ScriptEngine = new V8ScriptEngine();
            ScriptEngine.LoadSystemModules();             
        }

        /// <summary>
        /// 开始采集
        /// </summary>
        internal void Start()
        {
            ScriptEngine.Execute(Code);

            // 挂起当前线程
            // _lock.WaitOne();            
        }

        /// <summary>
        /// 立即结束采集
        /// </summary>
        internal void Stop()
        {
            Complete();

            // ScriptEngine.Dispose();
            try
            {
                Thread.CurrentThread.Abort();
            }
            catch { }
        }

        /// <summary>
        /// 设置采集结束
        /// </summary>
        internal void Complete()
        {
            // 唤醒主线程，并走完采集流程
            _lock.Release();            
        }

        private Semaphore _lock;
    }
}
