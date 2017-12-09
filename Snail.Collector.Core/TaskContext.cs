using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务执行环境上下文  
    /// </summary>
    public class TaskContext
    {
        internal TaskContext() { }

        /// <summary>
        /// 获取任务执行的工作目录
        /// </summary>
        public string ExecutePath { get; internal set; }

        /// <summary>
        /// 获取任务共享HttpClient
        /// </summary>
        public HttpClient HttpClient { get; internal set; }

        /// <summary>
        /// 获取当前任务ID
        /// </summary>
        public int TaskId { get; internal set; }

        /// <summary>
        /// 获取当前执行任务的V8引擎
        /// </summary>
        public V8ScriptEngine Engine { get; internal set; }

        /// <summary>
        /// 获取或设置新增任务总数
        /// </summary>
        public int NewTaskCount { get; set; }

        /// <summary>
        /// 获取或设置下载文件数
        /// </summary>
        public int DownFileCount { get; set; }      

        /// <summary>
        /// 将此上线文绑定到线程执行环境
        /// </summary>
        internal void BindContext()
        {
            ContextManager.SetTaskContext(this);
        }

        /// <summary>
        /// 获取当前任务上下文
        /// </summary>
        public static TaskContext Current
        {
            get
            {
                return ContextManager.GetTaskContext();
            }
        }
    }
}
