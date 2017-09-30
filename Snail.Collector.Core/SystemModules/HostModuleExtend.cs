using Microsoft.ClearScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void debug(object content)
        {
            System.Diagnostics.Debug.WriteLine(content);
        }
    }
}
