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

        private static object LockObj = new object();

        /// <summary>
        /// 读取某个配置文件的内容
        /// </summary>
        /// <param name="cfgFile"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取指定目录的文件名
        /// </summary>
        /// <param name="fullPath">完整物理路径或网络路径</param>
        /// <returns>返回文件名</returns>
        public static string GetFileName(string fullPath)
        {
            var idx = fullPath.LastIndexOf("\\");
            if (idx < 0)
            {
                idx = fullPath.LastIndexOf("/");
            }
            return fullPath.Substring(idx + 1);
        }

        /// <summary>
        /// 根据url获取截取的子目录
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static string[] GetPartDirectories(string url)
        {
            Uri uri = new Uri(url);
            return uri.AbsolutePath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 创建目录
        /// </summary>         
        /// <param name="fullPath">输出完整路径</param>
        /// <param name="dicParts">目录分部</param>
        /// <returns>返回是否创建成功</returns>
        public static bool CreateDirectory(out string fullPath, params string[] dicParts)
        {         
            fullPath = Path.Combine(dicParts);
            if (Directory.Exists(fullPath))
            {
                return true;
            }
            lock (LockObj)
            {
                var path = "";
                foreach (var item in dicParts)
                {
                    path = Path.Combine(path, item);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }    
            }
            return Directory.Exists(fullPath);
        }


        public static bool SafeCreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            lock (LockObj)
            {
                Directory.CreateDirectory(path);
            }
            return Directory.Exists(path);
        }
    }
}
