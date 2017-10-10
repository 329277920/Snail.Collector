
using Microsoft.ClearScript;
using System;
using System.Threading.Tasks;

namespace Snail.Collector.Core.SystemModules
{
    public class HostModuleExtend : ExtendedHostFunctions
    {                               
        /// <summary>
        /// 加载指定的模块到JS运行环境中
        /// </summary>
        /// <param name="module">模块名称</param>        
        public object require(string module)
        {
            return CollectorFactory.Current.ScriptEngine.LoadModule(module);            
        }

        /// <summary>
        /// 写入调试信息
        /// </summary>
        /// <param name="value"></param>
        public void debug(object value)
        {
            System.Diagnostics.Debug.WriteLine(value);
        }

        public void sleep(int millisecondsTimeout)
        {
            System.Threading.Thread.Sleep(millisecondsTimeout);
        }
    }
}
