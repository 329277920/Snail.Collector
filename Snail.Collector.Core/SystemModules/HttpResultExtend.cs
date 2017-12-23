using Snail.Collector.Html;
using Snail.Collector.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core.SystemModules
{
    /// <summary>
    /// Http返回值扩展对象
    /// </summary>
    public static class HttpResultExtend
    {
        public static Element toHtml(this HttpResult rest)
        {
            return new Element(rest.toString());
        }

        public static dynamic toJson(this HttpResult rest)
        {
            // JSON.parse(this)
            var strJson = rest.toString();
            if (string.IsNullOrEmpty(strJson))
            {
                return null;
            }
            return TaskInvokerContext.Current.Engine.Script.JSON.parse(strJson);            
        }
    }
}
