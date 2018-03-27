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
    public class TaskSource : IDisposable
    {
        /// <summary>
        /// 存储文件资源
        /// </summary>
        private Dictionary<int, ITaskSource> _source = new Dictionary<int, ITaskSource>();   

        /// <summary>
        /// 导入文件到资源中
        /// </summary>
        /// <param name="sourceIdx">指定资源Id</param>
        /// <param name="filePath">指定文件路径</param>
        /// <param name="encoding">文件编码</param>
        public void importFile(int sourceIdx, string filePath, string encoding = "utf-8")
        {
            var file = SnailCore.IO.PathUnity.GetFullPath(filePath);
            if (file == null)
            {
                throw new Exception(string.Format("未找到资源文件:'{0}'.", filePath));
            }
            if (this._source.ContainsKey(sourceIdx))
            {
                return;
            }
            lock (this)
            {
                if (this._source.ContainsKey(sourceIdx))
                {
                    return;
                }
                _source.Add(sourceIdx, new TaskSourceFileItem(file, Encoding.GetEncoding(encoding)));
            }
        }

        /// <summary>
        /// 从指定数据源中读取下一条记录
        /// </summary>
        /// <param name="sourceIdx">资源Id号</param>
        /// <returns></returns>
        public dynamic next(int sourceIdx)
        {
            if (!this._source.ContainsKey(sourceIdx))
            {
                throw new Exception(string.Format("未找到资源,编号:{0}.", sourceIdx));
            }
            var source = this._source[sourceIdx];
            return source.Next<dynamic>();
        }

        #region 资源释放

        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (this._source != null)
            {
                foreach (var kv in this._source)
                {
                    kv.Value.Dispose();
                }
            }

            if (disposing)
            {
                this._source.Clear();
            }
             
            disposed = true;
        }

        #endregion     
    }
}
