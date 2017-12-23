using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务事件参数
    /// </summary>
    public class TaskEventArgs : EventArgs
    {
        /// <summary>
        /// 获取当前触发事件的任务对象
        /// </summary>
        public Task Task { get; internal set; }
    }
}
