using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Html
{
    /// <summary>
    /// 节点迭代器
    /// </summary>
    public class ElementIterator
    {
        /// <summary>
        /// 迭代某个Element的所有子节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="callBack"></param>
        /// <param name="exit"></param>
        private static void Each(Element parentNode, Func<Element, bool> callBack, ref bool exit)
        {           
            var children = parentNode.children();
            if (children?.length > 0)
            {              
                for (var i = 0; i < children.length; i++)
                {                   
                    if (callBack != null)
                    {
                        if (!callBack.Invoke(children[i]))
                        {
                            exit = true;
                            return;
                        }
                    }                    
                    Each(children[i], callBack, ref exit);
                    if (exit)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 迭代某个Element的所有子节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="callBack"></param>
        internal static void Each(Element parentNode, Func<Element, bool> callBack)
        {
            var exit = false;
            Each(parentNode, callBack, ref exit);
        }
    }
}
