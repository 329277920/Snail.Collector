using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Snail.Collector.Common;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 采集统计信息（线程安全）
    /// </summary>
    public class CollectTaskState
    {
        private int _RunningTaskCount;
        /// <summary>
        /// 获取当前正在运行的任务数
        /// </summary>
        public int RunningTaskCount
        {
            get => this._lock.SafeReadValue(() => this._RunningTaskCount);
            set => this._lock.SafeSetValue(newValue => this._RunningTaskCount += newValue, value);
        }

        private int _ErrorTaskCount;
        /// <summary>
        /// 获取执行异常任务数
        /// </summary>
        public int ErrorTaskCount
        {
            get => this._lock.SafeReadValue(() => this._ErrorTaskCount);
            set => this._lock.SafeSetValue(newValue => this._ErrorTaskCount += newValue, value);
        }

        private int _CompleteTaskCount;
        /// <summary>
        /// 获取正常完成的任务数
        /// </summary>
        public int CompleteTaskCount
        {
            get => this._lock.SafeReadValue(() => this._CompleteTaskCount);
            set => this._lock.SafeSetValue(newValue => this._CompleteTaskCount += newValue, value);
        }

        private int _NewTaskCount;
        /// <summary>
        /// 获取当前新增任务数
        /// </summary>
        public int NewTaskCount
        {
            get => this._lock.SafeReadValue(() => this._NewTaskCount);
            set => this._lock.SafeSetValue(newValue => this._NewTaskCount += newValue, value);
        }

        private int _NewContentCount;
        /// <summary>
        /// 获取当前新增内容数
        /// </summary>
        public int NewContentCount
        {
            get => this._lock.SafeReadValue(() => this._NewContentCount);
            set => this._lock.SafeSetValue(newValue => this._NewContentCount += newValue, value);
        }

        private int _NewFileCount;
        /// <summary>
        /// 获取当前新增文件数
        /// </summary>
        public int NewFileCount
        {
            get => this._lock.SafeReadValue(() => this._NewFileCount);
            set => this._lock.SafeSetValue(newValue => this._NewFileCount += newValue, value);
        }

        /// <summary>
        /// 控制并发访问
        /// </summary>
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
    }
}
