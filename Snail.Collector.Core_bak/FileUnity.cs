using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snail.IO;
using System.IO;

namespace Snail.Collector.Core
{
    internal sealed class FileUnity
    {    
        public static string ReadConfigFile(string cfgFile)
        {
            var filePath = PathUnity.GetFullPath(cfgFile);
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new Exception(string.Format("cannot found file \"{0}\".", cfgFile));
            }

            var script = filePath.ReadToEnd(Encoding.UTF8);
            if (string.IsNullOrEmpty(script))
            {
                throw new Exception(string.Format("file is empty, \"{0}\".", filePath));
            }
            return script;
        }

        /// <summary>
        /// 通过文件路径获取目录
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetDrectory(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        /// <summary>
        /// 遍历某个路径的所有文件
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="find"></param>       
        public static void Each(DirectoryInfo dir, Action<FileInfo> callBack)
        {
            foreach (var file in dir.GetFiles())
            {
                callBack(file);
            }
            foreach (var childDir in dir.GetDirectories())
            {
                Each(childDir, callBack);
            }
        }
    }
}
