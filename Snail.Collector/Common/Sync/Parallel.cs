using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace Snail.Collector.Common.Sync
{
    /// <summary>
    /// 提供对并行计算的支持
    /// </summary>
    public sealed class Parallel : IDisposable
    {
        /// <summary>
        /// 获取最大并行度
        /// </summary>
        public int MaxParallelDegree
        {
            get;
            private set;
        }
       
        private int _runCount = 0;
        /// <summary>
        /// 获取当前执行任务的线程数
        /// </summary>
        public int RunningTaskCount
        {
            get
            {
                return this._runCount;
            }            
        }

        private Semaphore _pool;

        private Semaphore _poolWork;

        private Queue<WorkInfo> _works;

        private Queue<Thread> _thds;

        /// <summary>
        /// 初始化SnailCore.Sync.Parallel
        /// </summary>
        /// <param name="maxParallelDegree">设置最大并行度，该值应考虑在任务中是否有IO操作</param>        
        public Parallel(int maxParallelDegree)// todo: 未实现
        {
            this.MaxParallelDegree = maxParallelDegree;           
            this._pool = new Semaphore(this.MaxParallelDegree, this.MaxParallelDegree);
            this._poolWork = new Semaphore(0, this.MaxParallelDegree);
            this._thds = new Queue<Thread>();
            this._works = new Queue<WorkInfo>();
        }

        /// <summary>
        /// 对 System.Collections.IEnumerable{TSource} 执行 for each 操作，其中会并行运行迭代
        /// </summary>
        /// <typeparam name="TSource">源中数据的类型</typeparam>
        /// <param name="source">可枚举的数据源</param>
        /// <param name="body">将为每个迭代调用一次的委托</param>
        public void ForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
        {          
            var workCount = source.Count();
            SemaphoreSlim waitSG = new SemaphoreSlim(0, workCount);
            foreach (var item in source)
            {
                // 等待允许创建新线程，或空闲线程的信号 
                this._pool.WaitOne();
                var newThread = false;
                lock (this)
                {
                    if (this._thds.Count < this.MaxParallelDegree)
                    {
                        newThread = true;
                    }
                }
                if (newThread)
                {
                    var thd = this.CreateNewThread();
                    lock (this)
                    {
                        this._thds.Enqueue(thd);
                    }
                }
                lock (this)
                {
                    this._works.Enqueue(new WorkInfo(body, waitSG, item));
                }
                // 通知各线程任务入队列
                this._poolWork.Release();
            }
            for (var i = 0; i < workCount; i++)
            {
                waitSG.Wait();
            }
            waitSG.Dispose();        
        }

        /// <summary>
        /// 创建工作线程，在没有达到最大并发时，创建新线程
        /// </summary>
        private Thread CreateNewThread()
        {
            var thd = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    // 等待来自任务入队列的消息
                    this._poolWork.WaitOne();
                    Interlocked.Add(ref this._runCount, 1);
                    WorkInfo work = null;
                    lock (this)
                    {
                        work = this._works.Dequeue();
                    }
                    try
                    {
                        work.Method.DynamicInvoke(work.Params);
                    }
                    finally
                    {
                        // 通知任务结束
                        work?.Waiter.Release();
                        Interlocked.Add(ref this._runCount, -1);
                        // 通知主线程任务结束，进入空闲状态
                        this._pool.Release();
                    }
                }
            }));
            thd.IsBackground = true;
            thd.Start();
            return thd;
        }
              
        private class WorkInfo
        {
            public Delegate Method { get; set; }

            public object[] Params { get; set; }

            public SemaphoreSlim Waiter { get; set; }

            public WorkInfo(Delegate method, SemaphoreSlim waiter, params object[] parameters)
            {
                this.Method = method;
                this.Params = parameters;
                this.Waiter = waiter;
            }
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                while (this._thds.Count > 0)
                {
                    try
                    {
                        this._thds.Dequeue().Abort();
                    }
                    catch { }
                }
                this._pool.Dispose();
                this._poolWork.Dispose();
            }
            this._works.Clear();
            disposed = true;
        }

        ~Parallel()
        {
            Dispose(false);
        }
    }
}
