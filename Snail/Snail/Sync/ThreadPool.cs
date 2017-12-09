using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snail.Sync
{
    /// <summary>
    /// 自定义线程池
    /// </summary>
    public class ThreadPool
    {

        private int _thdCount;
        /// <summary>
        /// 获取当前已经创建的线程数量
        /// </summary>
        public int CreatedThreadCount
        {
            get { return _thdCount; }
        }

        /// <summary>
        /// 获取最大线程数
        /// </summary>
        public int MaxThreadCount
        {
            get;
            private set;
        }


        private int _runThdCount = 0;
        /// <summary>
        /// 获取正在运行的线程数
        /// </summary>
        public int RunningThreadCount
        {
            get { return _runThdCount; }
        }

        private Semaphore _lockWork;

        private Semaphore _lockThd;

        /// <summary>
        /// 当前需要执行的操作
        /// </summary>
        private Queue<WorkItem> _works;

        public ThreadPool(int maxCount)
        {
            this._thdCount = 0;
            this.MaxThreadCount = maxCount;
            this._lockWork = new Semaphore(maxCount, maxCount);
            this._lockThd = new Semaphore(0, maxCount);
            this._works = new Queue<WorkItem>();
        }

        /// <summary>
        /// 执行某个方法
        /// </summary>
        /// <param name="work">方法主体</param>
        /// <param name="state">方法参数</param>        
        public void Invoke(Action<object> work, object state)
        {
            // 控制并发任务数
            this._lockWork.WaitOne();

            // 任务入队列
            lock (this)
            {
                this._works.Enqueue(new WorkItem() { Work = work, State = state });
                this._lockThd.Release();
            }

            // 创建新线程已执行当前任务
            var isCreated = false;
            lock (this)
            {
                isCreated = this.RunningThreadCount == this.CreatedThreadCount &&
                    this.CreatedThreadCount < this.MaxThreadCount;
            }
            if (isCreated)
            {
                NewThread();
            }
        }

        /// <summary>
        /// 创建一个新线程
        /// </summary>
        private void NewThread()
        {
            new Thread(new ThreadStart(() =>
            {
                // 新建任务线程
                Interlocked.Increment(ref this._thdCount);

                while (true)
                {
                    // 等待新任务
                    this._lockThd.WaitOne();

                    // 任务状态 运行中
                    Interlocked.Increment(ref this._runThdCount);

                    // 获取任务
                    WorkItem work = null;
                    lock (this)
                    {
                        if (this._works.Count > 0)
                        {
                            work = this._works.Dequeue();
                        }
                    }

                    // 执行方法主体
                    if (work != null)
                    {
                        work.Work?.Invoke(work.State);                         
                    }

                    // 任务结束
                    Interlocked.Decrement(ref this._runThdCount);

                    // 通知并发任务允许进入
                    this._lockWork.Release();
                }
            }))
            { IsBackground = true }.Start();
        }

        private class WorkItem
        {
            /// <summary>
            /// 方法主体
            /// </summary>
            public Action<object> Work { get; set; }

            /// <summary>
            /// 方法参数
            /// </summary>
            public object State { get; set; }                          
        }
    }
}
