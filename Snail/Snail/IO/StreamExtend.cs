using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.IO
{
    /// <summary>
    /// 扩展字节流
    /// </summary>
    public static class StreamExtend
    {
        /// <summary>
        /// 按字节读取流，一直到末尾
        /// </summary>
        /// <param name="stream">当前字节流</param>
        /// <param name="bufferSize">读取缓冲区大小</param>
        /// <param name="readCallBack">每次读取的回调方法</param>
        public static async Task ReadToEndAsync(this Stream stream, int bufferSize, StreamReadCallBack readCallBack)
        {
            var buffer = new byte[bufferSize];
            var count = 0;
            while ((count = await stream.ReadAsync(buffer, 0, bufferSize).ConfigureAwait(false)) > 0)
            {
                if (!readCallBack(buffer, count))
                {
                    break;
                }
            }
        }
        

        /// <summary>
        /// 读取流中的所有字节
        /// </summary>
        /// <param name="stream">当前字节流</param>
        /// <param name="encode">编码方式</param>
        /// <param name="closeStream">在读取完成后关闭当前流</param>
        /// <returns>返回编码后的串</returns>
        public static async Task<string> ReadToEndAsync(this Stream stream, Encoding encode,bool closeStream = true)
        {
            using (StreamReader sr = new StreamReader(stream, encode))
            {
                var content = await sr.ReadToEndAsync().ConfigureAwait(false);
                if (closeStream)
                {
                    stream.Close();
                }
                return content;
            }               
        }
    }
}
