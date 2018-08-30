using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Html
{
    /// <summary>
    /// 节点集合
    /// </summary>
    public class ElementCollection : List<Element>
    {
        public ElementCollection() { }

        public ElementCollection(IEnumerable<Element> initEles)
        {
            foreach (var ele in initEles)
            {
                Add(ele);
            }
        }

        #region 导出属性

        public int length
        {
            get { return Count; }
        }

        public string innerHTML
        {
            get
            {
                if (this.Count <= 0)
                {
                    return string.Empty;
                }
                return this[0].innerHTML;
            }
        }

        public string outerHTML
        {
            get
            {
                if (this.Count <= 0)
                {
                    return string.Empty;
                }
                return this[0].outerHTML;
            }
        }

        public string innerText
        {
            get
            {
                if (this.Count <= 0)
                {
                    return string.Empty;
                }
                return this[0].innerText;
            }
        }

        #endregion

        #region 导出方法

        /// <summary>
        /// 选择子元素
        /// </summary>
        /// <returns></returns>
        public ElementCollection children()
        {
            if (this.Count <= 0)
            {
                return new Html.ElementCollection();
            }
            return this[0].children();          
        }

        public ElementCollection getElementsByTagName(string name, bool isFindAll = true)
        {
            var eles = new ElementCollection();
            ForEach(item =>
            {
                var children = item.getElementsByTagName(name, isFindAll);
                eles.AddRange(children);
            });
            return eles;
        }

        public ElementCollection getElementsByClassName(string name, bool isFindAll = true)
        {
            var eles = new ElementCollection();
            ForEach(item =>
            {
                var children = item.getElementsByClassName(name, isFindAll);
                eles.AddRange(children);
            });
            return eles;
        }

        public ElementCollection find(string selector)
        {
            var eles = new ElementCollection();
            ForEach(item =>
            {
                var children = item.find(selector);
                eles.AddRange(children);
            });
            return eles;
        }

        public string attr(string attrName, string value = null)
        {
            if (this.Count <= 0)
            {
                return string.Empty;
            }
            return this[0].attr(attrName, value);
        }

        /// <summary>
        /// 移除当前节点
        /// </summary>
        public void remove()
        {
            ForEach(item =>
            {
                item.remove();
            });
        }

        /// <summary>
        /// 移除某个属性
        /// </summary>
        /// <param name="attrs">属性名称</param>        
        public ElementCollection removeAttr(params string[] attrs)
        {
            ForEach(item =>
            {
                item.removeAttr(attrs);
            });
            return this;                
        }

        /// <summary>
        /// 移除标签
        /// </summary>
        /// <param name="tags">标签名称</param>                
        public ElementCollection removeTag(params string[] tags)
        {
            ForEach(item =>
            {
                item.removeTag(tags);
            });
            return this;
        }

        public void each(dynamic callBack)
        {
            foreach (var item in this)
            {
                callBack(item);
            }
        }

        #endregion
    }
}
