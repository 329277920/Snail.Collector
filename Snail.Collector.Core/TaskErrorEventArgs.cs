using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 在任务发生异常,未能启动时,事件参数
    /// </summary>
    public class TaskErrorEventArgs : TaskEventArgs
    {
        public Exception Ex { get; set; }
    }
}
