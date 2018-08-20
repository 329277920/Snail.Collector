using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snail.Collector
{
    /// <summary>
    /// 采集任务执行者列表，该列表用于去重采集页面
    /// </summary>
    public class CollectTaskInvokerList
    {
        private Dictionary<string, CollectTaskInvoker> _dic = new Dictionary<string, CollectTaskInvoker>();

        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// 添加一个采集执行者，当该页面不存在时
        /// </summary>
        /// <param name="collectTaskInvoker"></param>
        /// <returns></returns>
        public bool AddWhereNotExists(CollectTaskInvoker collectTaskInvoker)
        {
            return this._lock.SafeSetValue(item =>
            {
                var uri = item.Uri.ToString();
                if (!this._dic.ContainsKey(uri))
                {
                    this._dic.Add(uri, collectTaskInvoker);
                    return true;
                }
                return false;

            }, collectTaskInvoker);
        }
    }
}
