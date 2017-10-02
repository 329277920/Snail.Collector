using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Html
{
    public class HtmlModule
    {
        public Document load(string html)
        {
            var doc = new Document();
            doc.loadHtml(html);
            return doc;
        }
    }
}
