using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Common
{
    public class Log4NetLogger : ILogger
    {
        private Type _type = typeof(Log4NetLogger);

        /// <summary>
        /// 使用指定的配置文件初始化配置
        /// 如果配置节点在应用程序的config文件中，则不需要另外指定。
        /// 也可以在程序集属性中加入[assembly: log4net.Config.XmlConfigurator(Watch = true, ConfigFile = "")]
        /// </summary>
        /// <param name="config">log4net配置文件路径</param>
        public Log4NetLogger()
        {
            var config = "log4net.config";            
            if (!File.Exists(config))
            {
                throw new Exception(string.Format("not find config file : {0}", config));
            }
            XmlConfigurator.Configure(new FileInfo(config));
        }

        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="ex">异常对象</param>
        public void Error(string message, Exception ex = null)
        {
            LogManager.GetLogger(_type).Error(message, ex);
        }

        /// <summary>
        /// 写入普通日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public void Info(string message)
        {
            LogManager.GetLogger(_type).Info(message);
        }
    }
}
