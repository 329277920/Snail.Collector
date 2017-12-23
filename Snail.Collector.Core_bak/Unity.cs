using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    public class Unity
    {
        /// <summary>
        /// 读取资源文件
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="ass"></param>
        /// <returns></returns>
        public static string ReadResource(string resourceName, Assembly ass = null)
        {
            ass = ass ?? Assembly.GetExecutingAssembly();
            using (var stream = ass.GetManifestResourceStream(resourceName))
            {
                using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
