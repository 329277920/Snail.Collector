using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.IDE
{
    /// <summary>
    /// 任务统计项
    /// </summary>
    public class TaskStatisticsItem
    {
        /// <summary>
        /// 统计项索引号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 统计项接口地址
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 总请求数
        /// </summary>
        public int TotalReq { get; set; }

        /// <summary>
        /// 成功请求数
        /// </summary>
        public int TotalReqSuccess { get; set; }

        /// <summary>
        /// 失败请求数
        /// </summary>
        public int TotalReqError { get; set; }

        /// <summary>
        /// 第一次请求时间
        /// </summary>
        public DateTime FirstReqTime { get; set; }

        /// <summary>
        /// 最后一次请求时间
        /// </summary>
        public DateTime EndReqTime { get; set; }

        public TaskStatisticsItem(int idx, string uri)
        {
            this.Index = idx;
            this.Uri = uri;
            this.FirstReqTime = DateTime.MinValue;
            this.EndReqTime = DateTime.MinValue;
        }
    }
}
