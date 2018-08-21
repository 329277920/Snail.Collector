using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector
{
    /// <summary>
    /// 采集任务执行状态
    /// </summary>
    public enum CollectTaskStatus
    {
        None =0,
        Running = 1,
        Suspend = 2,
        Stop = 3
    }
}
