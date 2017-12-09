using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务存储实体
    /// </summary>
    public class TaskInvokerStorageEntity
    {
        /// <summary>
        /// 任务执行自增Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 所属任务Id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 执行地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 执行脚本
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        /// 执行状态(0:等待执行,1:正在执行,2:执行失败,3:执行成功)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后一次执行时间    
        /// </summary>
        public DateTime ExecTime { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int ExecCount { get; set; }
    }
}
