using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.IO
{
    public sealed class FileUnity
    {
        /// <summary>
        /// 异步读取文件内容
        /// </summary>
        /// <param name="file">文件对象</param>
        /// <param name="encode">编码方式</param>
        /// <returns></returns>
        public static async Task<string> ReadStringAsync(FileInfo file, Encoding encode)
        {
            return await ReadStringAsync(file.FullName, encode);
        }

        /// <summary>
        /// 异步读取文件内容
        /// </summary>
        /// <param name="file">文件对象</param>
        /// <param name="encode">编码方式</param>
        /// <returns></returns>
        public static async Task<string> ReadStringAsync(string file, Encoding encode)
        {
            var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);            
            var content = await fs.ReadToEndAsync(encode).ConfigureAwait(false);
            fs.Close();
            return content;
        }
    }
}
