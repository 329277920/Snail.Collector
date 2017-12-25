using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.IDE
{
    /// <summary>
    /// 文件资源项，暂时只支持一次加载内存，以支持随机模式查找
    /// </summary>
    public class TaskSourceFileItem : ITaskSource
    {
        /// <summary>
        /// 当前读取的指针
        /// </summary>
        private int _idx;

        private StreamReader sr;

        private FileStream fs;

        public TaskSourceFileItem(string filePath, Encoding encode)
        {
            this.fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            this.sr = new StreamReader(fs, encode);
        }

        public T Next<T>()
        {
            lock (this)
            {
                if (this.fs.Position == this.fs.Length)
                {
                    this.fs.Position = 0;
                }
                return this.sr.ReadLine();
            }
        }
    }
}
