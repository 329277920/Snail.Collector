using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using Snail.Text;

namespace Snail.Tester
{
    [TestClass]
    public class RegexExpressTester
    {
        [TestMethod]
        public void TestMethod()
        {
            Assert.IsTrue(new Regex(CommonRegexExpress.Numeric).IsMatch("00"));

            Assert.IsFalse(new Regex(string.Format(CommonRegexExpress.NumericLength, 2)).IsMatch(""));

            Assert.IsTrue(new Regex(string.Format(CommonRegexExpress.NumericRange, 2, 5)).IsMatch("12345"));

            Assert.IsFalse(new Regex(string.Format(CommonRegexExpress.NumericRange, 2, 5)).IsMatch("123456"));

            Assert.IsFalse(new Regex(string.Format(CommonRegexExpress.NumericRange, 2, 5)).IsMatch("1"));

            Assert.IsTrue(new Regex(string.Format(CommonRegexExpress.NumericRange, 2, 5)).IsMatch("12"));

            Assert.IsFalse(new Regex(string.Format(CommonRegexExpress.NumericRange, 2, 5)).IsMatch(""));

            Assert.IsTrue(new Regex(CommonRegexExpress.Chinese).IsMatch("中国人"));

            Assert.IsFalse(new Regex(CommonRegexExpress.Chinese).IsMatch(""));

            Assert.IsFalse(new Regex(CommonRegexExpress.Chinese).IsMatch("abc"));

            Assert.IsTrue(new Regex(CommonRegexExpress.NumericAndLetter).IsMatch("abc121"));

            Assert.IsTrue(new Regex(CommonRegexExpress.NumericAndLetter).IsMatch("abc"));

            Assert.IsTrue(new Regex(CommonRegexExpress.NumericAndLetter).IsMatch("678"));

            Assert.IsFalse(new Regex(CommonRegexExpress.NumericAndLetter).IsMatch("678_"));

            Assert.IsFalse(new Regex(CommonRegexExpress.NumericAndLetter).IsMatch(""));

            Assert.IsFalse(new Regex(CommonRegexExpress.NumericAndLetter).IsMatch(""));

            Assert.IsFalse(new Regex(CommonRegexExpress.Email).IsMatch("fsdfs1fds"));

            Assert.IsTrue(new Regex(CommonRegexExpress.Email).IsMatch("fsdf_21x011@fs123.com"));

            Assert.IsTrue(new Regex(CommonRegexExpress.Email).IsMatch("_fsdf_21x011@fs_123.cc"));
            

            //Assert.IsFalse(new Regex(CommonRegexExpress.Domain).IsMatch(""));

            //Assert.IsFalse(new Regex(CommonRegexExpress.Domain).IsMatch("ffwerw1"));

            //Assert.IsFalse(new Regex(CommonRegexExpress.Domain).IsMatch("www.qq."));

            //Assert.IsTrue(new Regex(CommonRegexExpress.Domain).IsMatch("www.qq.com"));

            //Assert.IsTrue(new Regex(CommonRegexExpress.Domain).IsMatch("qy.qq.com"));

            //Assert.IsFalse(new Regex(CommonRegexExpress.IP).IsMatch(""));

            //Assert.IsFalse(new Regex(CommonRegexExpress.IP).IsMatch("ffwerw1"));

            //Assert.IsFalse(new Regex(CommonRegexExpress.IP).IsMatch("10.56.65"));

            //Assert.IsTrue(new Regex(CommonRegexExpress.IP).IsMatch("192.168.1.30"));

            //Assert.IsTrue(new Regex(CommonRegexExpress.IP).IsMatch("10.62.69.32"));
        }
    }
}
