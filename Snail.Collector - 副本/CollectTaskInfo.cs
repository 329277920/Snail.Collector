using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector
{
    /// <summary>
    /// 采集任务实体
    /// </summary>
    public class CollectTaskInfo
    {
        /// <summary>
        /// 采集Id,唯一标识用户发起的每一次采集任务
        /// </summary>
        public int CollectId { get; set; }

        /// <summary>
        /// 采集页面地址
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 任务状态（0:未开始,1:正在采集,2:采集结束,3:采集失败）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 采集重试次数
        /// </summary>
        public int RetryCount { get; set; }
    }
}
