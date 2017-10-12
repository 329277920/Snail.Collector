using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Core
{
    /// <summary>
    /// 兼容js的异常实体定义
    /// </summary>
    public class JSException : Exception
    {
        public string message
        {
            get { return base.Message; }
        }

        public JSException(string message) : base(message) { }
    }
}
