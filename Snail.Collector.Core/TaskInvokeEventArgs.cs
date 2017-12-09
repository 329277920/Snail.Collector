using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务执行通知事件参数
    /// </summary>
    public class TaskInvokeEventArgs
    {
        /// <summary>
        /// 任务执行是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 任务执行附加消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 下载文件数
        /// </summary>
        public int DownFileCount { get; set; }

        /// <summary>
        /// 下载内容数
        /// </summary>
        public int DownContentCount { get; set; }
    }
}
