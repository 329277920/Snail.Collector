using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Html
{
    /// <summary>
    /// 正则表达式帮助类
    /// </summary>
    public sealed class RegexUnity
    {
        public string removeClass(string html)
        {
            return LazyClass.Value.Replace(html, "");
        }

        /// <summary>
        /// 属性选取器
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        internal static string AttrSelector(string selector)
        {
            var match = LazyAttr.Value.Match(selector);
            if (match.Success && match.Groups.Count >= 4 && match.Groups[2].Success)
            {
                return match.Groups[2].Value;
            }
            return null;
        }

        #region 存储正则表达式

        private static Lazy<Regex> LazyAttr = new Lazy<Regex>(() => 
        {
            return new Regex(@"(^\[)([a-zA-Z0-9_]*)(\]|.$)");
        }, true);

        private static Lazy<Regex> LazyClass = new Lazy<Regex>(() =>
        {
            return new Regex("class=['|\"]1.?['|\"]1");
        }, true);

        private static Lazy<Regex> LazyClass2 = new Lazy<Regex>(() =>
        {
            return new Regex(@"\.[\w\W]+");
        }, true);

        #endregion
    }
}
