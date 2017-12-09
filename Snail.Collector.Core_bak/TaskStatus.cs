using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 运行环境当前状态
    /// </summary>
    public enum TaskStatus
    {
        None = 0,
        Init = 1,
        Running = 2,
        /// <summary>
        /// 正在停止
        /// </summary>
        Stopping = 3,
        Stop = 4
    }
}
