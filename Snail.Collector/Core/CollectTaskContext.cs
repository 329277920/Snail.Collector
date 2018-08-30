using Snail.Collector.Common;
using System.Threading;


namespace Snail.Collector.Core
{
    /// <summary>
    /// 封装某一次执行任务上下文信息
    /// </summary>
    public class CollectTaskContext
    {
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
      
        /// <summary>
        /// 获取或设置任务执行状态
        /// </summary>
        public CollectTaskStatus TaskStatus { get; set; }

        private int _TotalCollectTaskInvokerCount;
        /// <summary>
        /// 获取或设置采集任务总数
        /// </summary>
        public int TotalCollectTaskInvokerCount
        {
            get {
                return this._lock.SafeReadValue(() => this._TotalCollectTaskInvokerCount);
            }
            set
            {
                this._lock.SafeSetValue(item => this._TotalCollectTaskInvokerCount = item, value);
            }
        }

        private int _CollectTaskInvokerCount;
        /// <summary>
        /// 获取或设置正在执行的采集任务数
        /// </summary>
        public int CollectTaskInvokerCount
        {
            get
            {
                return this._lock.SafeReadValue(() => this._CollectTaskInvokerCount);
            }
            set
            {
                this._lock.SafeSetValue(item => this._CollectTaskInvokerCount = item, value);
            }
        }      
    }
}
