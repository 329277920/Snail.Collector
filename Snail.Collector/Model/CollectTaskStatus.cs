using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Model
{
    /// <summary>
    /// 采集任务状态
    /// </summary>
    public enum CollectTaskStatus
    {
        /// <summary>
        /// 未开始
        /// </summary>
        None = 0,
        /// <summary>
        /// 进行中
        /// </summary>
        Running = 1,
        /// <summary>
        /// 已完成
        /// </summary>
        Complete = 2,
        /// <summary>
        /// 失败，但未超过重试次数
        /// </summary>
        Error = 3,
        /// <summary>
        /// 失败，且超过了重试次数
        /// </summary>
        Faild = 4
    }
}
