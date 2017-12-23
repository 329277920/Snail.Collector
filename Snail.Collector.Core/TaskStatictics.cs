using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务统计
    /// </summary>
    public sealed class TaskStatictics
    {
        /// <summary>
        /// 任务开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 下载文件数
        /// </summary>
        public int FileCount { get; set; }

        /// <summary>
        /// 执行任务数
        /// </summary>
        public int ExecTaskCount { get; set; }

        /// <summary>
        /// 异常任务数
        /// </summary>
        public int ErrTaskCount { get; set; }

        /// <summary>
        /// 生成子任务数
        /// </summary>
        public int NewTaskCount { get; set; }

        /// <summary>
        /// 获取文章数
        /// </summary>
        public int ArticleCount { get; set; }
    }
}
