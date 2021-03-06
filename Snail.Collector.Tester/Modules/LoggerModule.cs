﻿using log4net;
using log4net.Config;
using System;
using System.IO;

namespace Snail.Collector.Modules
{
    /// <summary>
    /// 日志模块
    /// </summary>
    public class LoggerModule
    {
        private Type _type = typeof(LoggerModule);

        /// <summary>
        /// 使用指定的配置文件初始化配置
        /// 如果配置节点在应用程序的config文件中，则不需要另外指定。
        /// 也可以在程序集属性中加入[assembly: log4net.Config.XmlConfigurator(Watch = true, ConfigFile = "")]
        /// </summary>
        /// <param name="config">log4net配置文件路径</param>
        public LoggerModule(string config = null)
        {
            if (string.IsNullOrEmpty(config))
            {
                config = "log4net.config";
            }
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
        public void error(string message, Exception ex = null)
        {
            LogManager.GetLogger(_type).Error(message, ex);
        }

        /// <summary>
        /// 写入普通日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public void info(string message)
        {
            LogManager.GetLogger(_type).Info(message);
        }
    }
}
