using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snail.IO
{
    /// <summary>
    /// IO扩展对象
    /// </summary>
    public static class IOExtend
    {
        /// <summary>
        /// 读取指定文件路径的文件内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> ReadToEndAsync(this string filePath, Encoding encoding)
        {
            return await FileUnity.ReadStringAsync(filePath, encoding).ConfigureAwait(false);
        }

        /// <summary>
        /// 读取指定文件路径的文件内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ReadToEnd(this string filePath, Encoding encoding)
        {
            return FileUnity.ReadStringAsync(filePath, encoding).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
