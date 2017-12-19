using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Common
{
    public interface ILogger
    {
        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Error(string source, string message, Exception ex = null);
    }
}
