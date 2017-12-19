using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Common
{
    /// <summary>
    /// 提供对日志操作的代理
    /// </summary>
    public sealed class LoggerProxy
    {
        private static ILogger Logger;

        public static void RegLogger(ILogger logger)
        {
            Logger = logger;
        }

        public static void Error(string source, string content, Exception ex = null)
        {
            Logger.Error(source, content, ex);
        }
    }
}
