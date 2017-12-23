using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务执行者统计实体
    /// </summary>
    public class TaskItemStatEntity
    {
        /// <summary>
        /// 执行者ID
        /// </summary>
        public int InvokerId { get; set; }

        /// <summary>
        /// 下载文件数量
        /// </summary>
        public int FileCount { get; set; }

        /// <summary>
        /// 文章数量
        /// </summary>
        public int ArticleCount { get; set; }

        /// <summary>
        /// 子任务数量
        /// </summary>
        public int ItemCount { get; set; }
    }
}
