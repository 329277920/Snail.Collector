using Snail.Collector.Common;
using System;

namespace Snail.Collector.Modules
{
    /// <summary>
    /// 日志模块
    /// </summary>
    public class LoggerModule
    {       
        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="ex">异常对象</param>
        public void error(string message, Exception ex = null)
        {
            TypeContainer.Resolve<ILogger>().Error(message, ex);
        }

        /// <summary>
        /// 写入普通日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public void info(string message)
        {
            TypeContainer.Resolve<ILogger>().Info(message);
        }
    }
}
