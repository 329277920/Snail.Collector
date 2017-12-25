using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.IDE
{
    /// <summary>
    /// 任务执行资源管理类
    /// </summary>
    public class TaskSource
    {
        /// <summary>
        /// 存储文件资源
        /// </summary>
        private Dictionary<string, string[]> sourceFromFile;

        /// <summary>
        /// 导入文件到资源中
        /// </summary>
        /// <param name="filePath"></param>
        public void importFile(string filePath, string encoding = "utf-8")
        {
            var file = Snail.IO.PathUnity.GetFullPath(filePath);
            if (file == null)
            {
                throw new Exception(string.Format("未找到资源文件:'{0}'.", filePath));
            }
            if (this.sourceFromFile.ContainsKey(filePath))
            {
                return;
            }
            lock (this)
            {
                if (this.sourceFromFile.ContainsKey(filePath))
                {
                    return;
                }
                var encode = Encoding.GetEncoding(encoding);               
            }
        }

        private string[] readFile(string filePath, Encoding encode)
        {
            List<string> conts = new List<string>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs, encode))
                {
                    sr.ReadLine();
                }
            }
        }
    }
}
