using System;
using System.Collections.Generic;
using System.Text;

namespace Snail.Text
{
    /// <summary>
    /// 常用正则表达式定义
    /// </summary>
    public class CommonRegexExpress
    {
        /// <summary>
        /// 只允许出现数字
        /// </summary>
        public const string Numeric = "^[0-9]+$";

        /// <summary>
        /// 只允许出现n位数字
        /// </summary>
        public const string NumericLength = @"^\d{{{0}}}$";

        /// <summary>
        /// 只允许出现n-m位数字
        /// </summary>
        public const string NumericRange = @"^\d{{{0},{1}}}$";

        /// <summary>
        /// 只允许出现中文
        /// </summary>
        public const string Chinese = @"^[\u4e00-\u9fa5]+$";

        /// <summary>
        /// 只允许出现数字和字母
        /// </summary>
        public const string NumericAndLetter = @"^[A-Za-z0-9]+$";

        /// <summary>
        /// 匹配邮箱地址
        /// </summary>
        public const string Email = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        /// <summary>
        /// 匹配域名
        /// </summary>
        // public const string Domain = @"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(/.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+/.?";      

    }
}
