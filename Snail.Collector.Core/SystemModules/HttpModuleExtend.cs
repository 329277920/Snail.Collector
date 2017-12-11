
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

        public override bool getFile(params dynamic[] files)
        {
            var invokerContext = ContextManager.GetTaskInvokerContext();
            if (invokerContext == null)
            {
                // todo: 写入日志
                return false;
            }
            var result = base.getFile(files);
            if (result)
            {
                invokerContext.TaskContext.SetStat(files.Length, TaskStatTypes.File);
            }
            return result;
        }
    }
}
