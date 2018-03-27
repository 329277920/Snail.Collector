using SnailCore.Data;
using SnailCore.IO;
using System;
using System.Text;


namespace Snail.Collector.Core.Configuration
{
    public sealed class ConfigManager
    {
        public static ConfigInfo Current;

        /// <summary>
        /// 系统默认编码
        /// </summary>      
        public static Encoding DefulatEncoding { get { return Encoding.UTF8; } }
        
        static ConfigManager()
        {
            
            var cfgFile = PathUnity.GetFullPath("config.json");
            if (string.IsNullOrEmpty(cfgFile))
            {
                throw new Exception(string.Format("未找到文件:{0}.", "config.json"));
            }
            var strCfg = cfgFile.ReadToEnd(DefulatEncoding);
            Current = Serializer.JsonDeserialize<ConfigInfo>(strCfg);
        }
    }
}
