using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Snail.Collector.Modules.Html
{
    /// <summary>
    /// 在脚本中访问的Element对象
    /// </summary>
    public class Element
    {
        private const string LogSource = "htmlParse";

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

        public string tagName
        {
            get { return _innerNode?.OriginalName; }
        }

        /// <summary>
        /// 选择子元素
        /// </summary>
        /// <returns></returns>
        public ElementCollection children => new ElementCollection(from item in _innerNode.ChildNodes
                                                                   select new Element(item));


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
            return Select(this, (item) => item.tagName == name, null, isFindAll);
        }

        public ElementCollection find(string selector)
        {
            if (string.IsNullOrEmpty(selector))
            {
                return new ElementCollection();
            }
            return new ElementCollection(from node in _innerNode.QuerySelectorAll(selector)
                                         select new Element(node));
        }

        /// <summary>
        /// 选择不包含某个特性的所有标签
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public ElementCollection not(string selector)
        {
            if (string.IsNullOrEmpty(selector))
            {
                return new ElementCollection();
            }
            // 不包含某个属性
            if (selector.StartsWith("[") && selector.EndsWith("]"))
            {
                var attrName = selector.Substring(1).Substring(0, selector.Length - 2);
                return this.Select(this, (item) => {                    
                    foreach (var attr in item._innerNode.Attributes)
                    {
                        if (attr.Name.Equals(attrName))
                        {
                            return false;
                        }
                    }
                    return true;
                });
            }
            return new ElementCollection();
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
        /// 移除某个属性
        /// </summary>
        /// <param name="attrName">属性名称</param>        
        public Element removeAttr(params string[] attrs)
        {
            if (attrs == null || attrs.Length <= 0)
            {
                return this;
            }
            this.innerRemoveAttr(this, attrs);

            ElementIterator.Each(this, (child) =>
            {
                this.innerRemoveAttr(child, attrs);
                return true;
            });

            return this;
        }

        /// <summary>
        /// 移除标签
        /// </summary>
        /// <param name="tagNames">标签名称</param>           
        public Element removeTag(params string[] tags)
        {
            if (tags == null || tags.Length <= 0)
            {
                return this;
            }
            innerRemoveTag(this, (from item in tags select item.ToLower()).ToArray());
            return this;            
        }
        
        private void innerRemoveTag(Element target, params string[] tags)
        {
            ElementCollection children = target.children;
            for (var i = 0; i < children.length; i++)
            {
                var tag = children[i].tagName?.ToLower();
                if (!string.IsNullOrEmpty(tag) && tags.Contains(tag))
                {
                    children[i].remove();
                    continue;
                }
                innerRemoveTag(children[i], tags);
            }
        }

        private void innerRemoveAttr(Element target, params string[] attrs)
        {
            foreach (var name in attrs)
            {
                var attr = target._innerNode.Attributes[name];
                if (attr != null)
                {
                    target._innerNode.Attributes.Remove(attr);
                }
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
            var children = parentNode.children;
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
