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
    public class TaskSourceFileItem : ITaskSource, IDisposable
    {
        /// <summary>
        /// 当前读取的指针
        /// </summary>
        private int _idx;

        private StreamReader sr;

        private FileStream fs;

        private string filePath;

        private Encoding currEncoding;

        public TaskSourceFileItem(string filePath, Encoding encode)
        {           
            this.filePath = filePath;
            this.currEncoding = encode;

            this.Init();
        }      

        public T Next<T>()
        {            
            lock (this)
            {
                while (true)
                {                    
                    var value = this.sr.ReadLine();                     
                    if (!string.IsNullOrEmpty(value))
                    {
                        return Snail.Data.Serializer.JsonDeserialize<T>(value);
                    }
                    if (this.fs.Position == this.fs.Length)
                    {
                        this.Dispose();
                        this.Init();
                    }
                }
            }
        }

        public void Dispose()
        {
            this.sr?.Dispose();
            this.fs?.Dispose();
        }

        private void Init()
        {
            this.fs = new FileStream(this.filePath, FileMode.Open, FileAccess.Read);
            this.sr = new StreamReader(fs, this.currEncoding);
        }
    }
}
