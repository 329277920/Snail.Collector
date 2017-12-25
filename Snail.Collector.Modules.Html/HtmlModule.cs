using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Html
{
    /// <summary>
    /// Html解析模块
    /// </summary>
    public class HtmlModule
    {
        /// <summary>
        /// 加载网页并返回节点操作对象Element
        /// </summary>
        /// <param name="html">html代码</param>
        /// <returns>返回Element</returns>
        public Element load(string html)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            return new Element(doc.DocumentNode);
        }

        /// <summary>
        /// 去掉html中的脚本块
        /// </summary>
        /// <param name="html">html代码</param>
        /// <returns></returns>
        public string removeScript(string html)
        {
            return LazyRegScript.Value.Replace(html, string.Empty);
        }

        public string removeTags(string html)
        {
            return LazyRegTags.Value.Replace(removeScript(html), string.Empty);
        }

        #region 存储正则表达式

        private static Lazy<Regex> LazyRegScript = new Lazy<Regex>(() =>
        {
            return new Regex("<script.*?>.*?</script>");
        }, true);

        private static Lazy<Regex> LazyRegTags = new Lazy<Regex>(() =>
        {
            return new Regex("<(?!img|br|p|/p|div|/div|table|/table).*?>");
        }, true);

        #endregion
    }
}
