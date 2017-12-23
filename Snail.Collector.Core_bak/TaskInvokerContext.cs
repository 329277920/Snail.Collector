using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务执行者上下文
    /// </summary>
    public class TaskInvokerContext
    {
        /// <summary>
        /// 获取任务绑定的脚本引擎
        /// </summary>
        public V8ScriptEngine Engine { get; internal set; }

        /// <summary>
        /// 获取或设置当前任务存储对象
        /// </summary>
        public TaskInvokerStorageEntity TaskInvokerInfo { get; set; }

        /// <summary>
        /// 获取当前任务执行上下文
        /// </summary>
        public TaskContext TaskContext { get; internal set; }
       
    }
}
