using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Html
{
    /// <summary>
    /// 在脚本中访问的Element对象
    /// </summary>
    public class Element
    {
        internal Element(HtmlNode node)
        {
            _innerNode = node;
        }

        public Element getElementById(string id)
        {
            return Single(this._innerNode, (item) => item.Id == id);
        }

        public ElementCollection getElementsByClassName(string className,bool isFindAll = false)
        {
            return Select(this._innerNode, (item) => item.GetAttributeValue("class", "").Split(' ').Contains(className), null, isFindAll);
        }

        public ElementCollection getElementsByTagName(string name, bool isFindAll = false)
        {
            return Select(this._innerNode, (item) => item.Name == name, null, isFindAll);
        }

        public string getAttribute(string name)
        {
            return _innerNode.GetAttributeValue(name, string.Empty);
        }

        public string innerHTML
        {
            get { return _innerNode?.InnerHtml; }
        }

        public string innerText
        {
            get { return _innerNode?.InnerText; }
        }

        #region 私有成员

        private HtmlNode _innerNode;

        private Element() { }

        internal static Element Single(HtmlNode parentNode, Func<HtmlNode, bool> filtter)
        {
            if (parentNode?.ChildNodes?.Count > 0)
            {
                foreach (var childNode in parentNode.ChildNodes)
                {
                    if (filtter(childNode))
                    {
                        return new Element(childNode);
                    }
                    var node = Single(childNode, filtter);
                    if (node != null)
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        internal ElementCollection Select(HtmlNode parentNode, Func<HtmlNode, bool> filtter, List<Element> nodeContainer = null,bool isFindAll = false)
        {
            if (nodeContainer == null)
            {
                nodeContainer = new List<Element>();
            }
            if (parentNode?.ChildNodes?.Count > 0)
            {
                foreach (var childNode in parentNode.ChildNodes)
                {
                    if (filtter(childNode))
                    {
                        nodeContainer.Add(new Element(childNode));
                    }
                    if (isFindAll)
                    {
                        Select(childNode, filtter, nodeContainer);
                    }                    
                }
            }
            return new ElementCollection(nodeContainer);
        }

        #endregion        
    }
}
