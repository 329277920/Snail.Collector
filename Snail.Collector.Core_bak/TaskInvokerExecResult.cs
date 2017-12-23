using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务执行者执行结果 // todo: 细化执行结果
    /// </summary>
    public class TaskInvokerExecResult
    {
        /// <summary>
        /// 获取或设置任务执行是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 获取或设置此次执行对应的存储对象
        /// </summary>
        public TaskInvokerStorageEntity TaskInvoker { get; set; }

        public TaskInvokerExecResult()
        {
            this.Success = false;            
        }
    }
}
