using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Snail.Collector.Html
{
    /// <summary>
    /// 在脚本中访问的Element对象
    /// </summary>
    public class Element
    {
        public Element(HtmlNode node)
        {
            _innerNode = node;            
        }

        public Element(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            _innerNode = doc.DocumentNode;
        }

        #region 导出属性

        public string innerHTML
        {
            get => _innerNode.InnerHtml; 
            set => _innerNode.InnerHtml = value;
        }

        public string outerHTML
        {
            get => _innerNode.OuterHtml;           
        }

        public string innerText
        {
            get { return _innerNode?.InnerText; }
        }
       

        #endregion

        #region 导出方法

        public Element getElementById(string id)
        {
            return Single(this, (item) => item.attr("id") == id);
        }

        public ElementCollection getElementsByClassName(string className, bool isFindAll = true)
        {
            return Select(this, (item) => item.attr("class").Split(' ').Contains(className), null, isFindAll);
        }

        public ElementCollection getElementsByTagName(string name, bool isFindAll = true)
        {
            return Select(this, (item) => item.attr("name") == name, null, isFindAll);
        }

        public ElementCollection find(string selector)
        {
            if (string.IsNullOrEmpty(selector))
            {
                return new ElementCollection();
            }

            // 选择某个属性
            var css = RegexUnity.AttrSelector(selector);
            if (!string.IsNullOrEmpty(css))
            {
                ElementCollection eles = new ElementCollection();
                ElementIterator.Each(this, (item) => 
                {
                    if (!string.IsNullOrEmpty(item.attr(css)))
                    {
                        eles.Add(item);
                    }
                    return true;
                });
                return eles;
            }         
            return new ElementCollection(from node in _innerNode.QuerySelectorAll(selector)
                                         select new Element(node));
        }

        /// <summary>
        /// 移除当前节点
        /// </summary>
        public void remove()
        {
            _innerNode.Remove();
        }

        public string attr(string attrName, string value = null)
        {
            if (value == null)
            {              
                return _innerNode.GetAttributeValue(attrName, "");
            }
            else
            {
                _innerNode.SetAttributeValue(attrName, value);
                return value;
            }
        }

        /// <summary>
        /// 选择子元素
        /// </summary>
        /// <returns></returns>
        public ElementCollection children()
        {
            return new ElementCollection(from item in _innerNode.ChildNodes
                                         select new Element(item));
        }

        public void removeClass()
        {
            var html = this.outerHTML;
            if (string.IsNullOrEmpty(html))
            {
                return;
            }
           
        }

        #endregion

        #region 私有成员

        private HtmlNode _innerNode;

        private Element() { }

        internal static Element Single(Element parentNode, Func<Element, bool> filtter)
        {
            Element target = null;
            ElementIterator.Each(parentNode, (item) => 
            {
                if (filtter(item))
                {
                    target = item;
                    return true;
                }
                return false;
            });
            return target;       
        }

        internal ElementCollection Select(Element parentNode, Func<Element, bool> filtter, List<Element> nodeContainer = null,bool isFindAll = true)
        {
            if (nodeContainer == null)
            {
                nodeContainer = new List<Element>();
            }
            var children = parentNode.children();
            if (children.Count > 0)
            {
                foreach (var childNode in children)
                {
                    if (filtter(childNode))
                    {
                        nodeContainer.Add(childNode);
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
