using Snail.Collector.Common;
using Snail.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 异常管理器
    /// </summary>
    public class ErrorMananger : Common.ILogger
    {
        public static ErrorMananger Instance = new ErrorMananger();

        /// <summary>
        /// 在发生异常时触发
        /// </summary>
        public event EventHandler<ErrorEventArgs> OnOccursError;

        public void Init()
        {
            LoggerProxy.RegLogger(this);
        }

        public void Error(string source, string message, Exception ex = null)
        {
            Logger.Error(string.Format("[{0}] {1}", source.ToString(), message), ex);
            try
            {
                this.OnOccursError?.Invoke(this, new ErrorEventArgs(source, message, ex));
            }
            catch (Exception cbEx)
            {
                Logger.Error(string.Format("[{0}] {1}", "errorManager", "执行异常回调失败"), cbEx);
            }
        }
    }
}
