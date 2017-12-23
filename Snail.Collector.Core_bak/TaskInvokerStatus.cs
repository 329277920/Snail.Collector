using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务执行状态
    /// </summary>
    public enum TaskInvokerStatus
    {     
        Init = 0,
        Running = 1,
        Stop = 2
    }
}
