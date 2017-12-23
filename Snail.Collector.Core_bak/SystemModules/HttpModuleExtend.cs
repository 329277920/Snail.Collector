
using Snail.Collector.Html;
using Snail.Collector.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core.SystemModules
{
    public class HttpModuleExtend : HttpModule
    {
        public Element getDoc(string uri)
        {
            var html = this.getStr(uri);        
            return new Element(html);
        }
    }
}
