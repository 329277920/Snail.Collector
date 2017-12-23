using System;
using System.Collections.Generic;
using System.Text;

using _Path = System.IO.Path;
namespace Snail.IO
{
    /// <summary>
    /// 路径帮助类
    /// </summary>
    public sealed class PathUnity
    {
        private static Lazy<Type> CurrType => new Lazy<Type>(() => typeof(PathUnity), true);

        /// <summary>
        /// 在程序执行目录下查找指定的文件或目录
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>返回完整路径地址，如果未找到则返回空</returns>
        public static string GetFullPath(string path)
        {
            var fullPath = _Path.GetFullPath(path);
            if (System.IO.File.Exists(fullPath) || System.IO.Directory.Exists(fullPath))
            {
                return fullPath;
            }
            fullPath = _Path.Combine(_Path.GetDirectoryName(CurrType.Value.Assembly.Location), fullPath);
            if (System.IO.File.Exists(fullPath) || System.IO.Directory.Exists(fullPath))
            {
                return fullPath;
            }
            fullPath = _Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, fullPath);
            if (System.IO.File.Exists(fullPath) || System.IO.Directory.Exists(fullPath))
            {
                return fullPath;
            }
            fullPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin", fullPath);
            if (System.IO.File.Exists(fullPath) || System.IO.Directory.Exists(fullPath))
            {
                return fullPath;
            }
            return null;
        }
    }
}
