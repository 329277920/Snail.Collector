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
    public class TaskErrorMananger : Common.ILogger
    {
        public static TaskErrorMananger Instance = new Lazy<TaskErrorMananger>(() =>
        {
            var instance = new TaskErrorMananger();
            instance.Init();
            return instance;
        }, true).Value;

        private TaskErrorMananger() { }

        /// <summary>
        /// 在发生异常时触发
        /// </summary>
        public event EventHandler<ErrorEventArgs> OnOccursError;

        private bool _isInit = false;
        public void Init()
        {
            if (this._isInit)
            {
                return;
            }
            lock (this)
            {
                if (this._isInit)
                {
                    return;
                }
                LoggerProxy.RegLogger(this);
                this._isInit = true;
            }          
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

        /// <summary>
        /// 写入提示日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public void Info(string source, string message)
        {
            Logger.Info(string.Format("[{0}] {1}", source.ToString(), message));
        }
    }
}
