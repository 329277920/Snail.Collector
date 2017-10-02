using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Html
{
    /// <summary>
    /// 在脚本中访问的Document对象
    /// </summary>
    public class Document
    {
        private HtmlDocument _innerDoc;
        public Element documentElement
        {
            get;
            private set;
        }

        public Document()
        {
            _innerDoc = new HtmlDocument();
            documentElement = new Element(_innerDoc.DocumentNode);
        }

        public void loadHtml(string html)
        {
            this._innerDoc.LoadHtml(html);
            documentElement = new Element(_innerDoc.DocumentNode);
        }

        public Element getElementById(string id)
        {
            return documentElement.getElementById(id);
        }

        public ElementCollection getElementsByClassName(string className)
        {
            return new ElementCollection(documentElement.getElementsByClassName(className));
        }

        public ElementCollection getElementsByTagName(string name)
        {
            return new ElementCollection(documentElement.getElementsByTagName(name));
        }
    }
}
