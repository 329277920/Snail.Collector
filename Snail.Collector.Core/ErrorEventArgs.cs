using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 异常发生时的附加信息
    /// </summary>
    public class ErrorEventArgs : EventArgs
    {
        /// <summary>
        /// 获取当前执行的任务Id
        /// </summary>
        public int TaskId { get; internal set; }

        /// <summary>
        /// 获取当前任务执行的Url
        /// </summary>
        public string Url { get; internal set; }

        /// <summary>
        /// 获取异常源
        /// </summary>
        public string Source { get; internal set; }       

        /// <summary>
        /// 获取异常消息
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// 获取异常对象
        /// </summary>
        public Exception Error { get; internal set; }

        public ErrorEventArgs(string source, string message, Exception ex = null)
        {
            this.TaskId = TaskContext.Current != null ? TaskContext.Current.TaskId : 0;
            this.Url = TaskInvokerContext.Current?.TaskInvokerInfo?.Url;
            this.Source = source;        
            this.Message = message;
            this.Error = ex;
        }
    }
}
