using Snail.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 采集任务执行结束事件参数
    /// </summary>
    public class CollectTaskInvokeCompleteArgs
    {
        /// <summary>
        /// 任务是否成功完成
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 任务执行失败信息
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// 当前任务实体
        /// </summary>
        public CollectTaskInfo Task { get; set; }
    }
}
