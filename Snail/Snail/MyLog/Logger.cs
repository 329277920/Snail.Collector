using System;

namespace Snail.Log
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    public sealed class Logger
    {               
        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="ex">异常对象</param>
        public static void Error(string message, Exception ex = null)
        {
            SafeInvoke((obj) =>
            {
                var ps = obj as Tuple<string, Exception>;

                GetLogger().Error(ps.Item1, ps.Item2);

            }, new Tuple<string, Exception>(message,ex)); 
        }

        /// <summary>
        /// 写入普通日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Info(string message)
        {
            SafeInvoke((obj) =>
            {
                GetLogger().Info(obj as string);

            }, message);
        }
       
        /// <summary>
        /// 在Debug模式中，将信息写入输出窗口
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Debug(string message)
        {
            SafeInvoke((obj) =>
            {
                GetLogger().Debug(obj as string);

            }, message);
        }

        /// <summary>
        /// 安全执行某个方法，将错误信息输出到调试窗口
        /// </summary>
        /// <param name="invoke"></param>
        /// <param name="value"></param>
        private static void SafeInvoke(Action<object> invoke,object value)
        {
            try
            {
                invoke(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private static Lazy<ILogger> LoggerInstance = new Lazy<ILogger>(() => 
        {
            return new Log4NetLogger();
        }, true);

        private static ILogger GetLogger()
        {
            return LoggerInstance.Value;
        }
    }
}
