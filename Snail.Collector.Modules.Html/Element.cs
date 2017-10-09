using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapySharp.Extensions;

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

        public ElementCollection getElementsByClassName(string className,bool isFindAll = true)
        {
            return Select(this._innerNode, (item) => item.GetAttributeValue("class", "").Split(' ').Contains(className), null, isFindAll);
        }

        public ElementCollection getElementsByTagName(string name, bool isFindAll = true)
        {
            return Select(this._innerNode, (item) => item.Name == name, null, isFindAll);
        }

        public ElementCollection css(string selector)
        {             
            return new ElementCollection(from node in this._innerNode.CssSelect(selector)
                                         select new Element(node));
        }

        public string attr(string attrName, string value = null)
        {
            if (value == null)
            {
                return _innerNode.GetAttributeValue(attrName);
            }
            else
            {
                _innerNode.SetAttributeValue(attrName, value);
                return value;
            }
        }

        public string innerHTML
        {
            get { return _innerNode?.InnerHtml; }
        }

        public string OuterHTML
        {
            get { return _innerNode?.OuterHtml; }
        }

        public string innerText
        {
            get { return _innerNode?.InnerText; }
        }

        public string OuterHtml
        {
            get { return _innerNode?.OuterHtml; }
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

        internal ElementCollection Select(HtmlNode parentNode, Func<HtmlNode, bool> filtter, List<Element> nodeContainer = null,bool isFindAll = true)
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
                        Select(childNode, filtter, nodeContainer, isFindAll);
                    }                    
                }
            }
            return new ElementCollection(nodeContainer);
        }

        #endregion        
    }
}
