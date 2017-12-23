using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 模块定义
    /// </summary>
    internal class ModuleInfo
    {
        /// <summary>
        /// 获取或设置此模块在JS引擎中的访问名称
        /// </summary>
        public string Name { get; set; }       

        /// <summary>
        /// 获取或设置模块类型
        /// </summary>
        public string Type { get; set; }  
        
        /// <summary>
        /// 获取或设置模块程序集
        /// </summary>
        public string Assembly { get; set; } 
        
        /// <summary>
        /// 获取或设置模块代理脚本
        /// </summary>
        public string ProxyScript { get; set; }  
        
        /// <summary>
        /// 获取或设置模块执行目录
        /// </summary>
        public string ExecutePath { get; set; }
    }
}
