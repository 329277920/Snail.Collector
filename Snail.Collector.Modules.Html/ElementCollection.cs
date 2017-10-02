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

        public int length {
            get { return Count; }
        }

        public ElementCollection getElementsByTagName(string name, bool isFindAll = false)
        {
            var eles = new ElementCollection();
            ForEach(item => 
            {
                var children = item.getElementsByTagName(name, isFindAll);
                eles.AddRange(children);
            });
            return eles;
        }

        public ElementCollection getElementsByClassName(string name, bool isFindAll = false)
        {
            var eles = new ElementCollection();
            ForEach(item =>
            {
                var children = item.getElementsByClassName(name, isFindAll);
                eles.AddRange(children);
            });
            return eles;
        }
    }
}
