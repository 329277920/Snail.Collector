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
        public static void Error(string source, string content, Exception ex = null)
        {
            Snail.Log.Logger.Error(string.Format("[{0}] {1}", source, content), ex);
        }
    }
}
