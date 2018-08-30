using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules
{
    public interface IInitModule
    {
        /// <summary>
        /// 在加入到引擎前，执行的初始化操作
        /// </summary>
        /// <param name="scriptEngine"></param>
        void Init(V8ScriptEngine scriptEngine);
    }
}
