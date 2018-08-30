using Snail.Collector.Core;
using Snail.Collector.Log;
using Snail.Collector.Model;
using Snail.Collector.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using Snail.Collector.Common;
using System.Threading.Tasks;

namespace Snail.Collector
{
    /// <summary>
    /// 采集任务工厂
    /// </summary>
    public class CollectTaskRuntime
    {
        private ICollectRepository _collectDal;
        private ICollectTaskRepository _collectTaskDal;
        private ILogger _logger;
        private Queue<CollectTaskInvoker> _invokers;
        private Semaphore _sh;
        private ReaderWriterLockSlim _lock;
        private CollectInfo _collect;
        private int _workCount = 0;

        public event EventHandler OnStart;

        public event EventHandler OnStop;

        /// <summary>
        /// 获取采集任务上下文
        /// </summary>
        protected CollectTaskContext Context { get; private set; }

        public CollectTaskRuntime(
            ICollectRepository collectDal, 
            ICollectTaskRepository collectTaskDal,
            ILogger logger)
        {
            this._collectDal = collectDal;
            this._collectTaskDal = collectTaskDal;
            this._logger = logger;
            this._sh = new Semaphore(20, 20);
            this._invokers = new Queue<CollectTaskInvoker>();
            this._lock = new ReaderWriterLockSlim();
            this.Context = new CollectTaskContext() { TaskStatus = CollectTaskStatus.None };        
        }

        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="collectInfo"></param>
        public void Start(CancellationToken cancellationToken, CollectInfo collectInfo)
        {
            this._collect = collectInfo;

            // 执行初始化脚本，不做异常捕获，发生异常直接退出
            using (var initInvoker = new CollectTaskInvoker())
            {
                initInvoker.Invoke(collectInfo, null);
            }
            return;
            if (cancellationToken.IsCancellationRequested)
            {
                this.SetStopStatus();
            }

            // 循环执行
            while (true)
            {
                // 取消执行，等待其他任务结束
                if (cancellationToken.IsCancellationRequested)
                {
                    if (this._lock.SafeReadValue(() => this._workCount) > 0)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    break;
                }

                // 请求执行
                this._sh.WaitOne();            
                var taskInfo = this._collectTaskDal.SelectSingle(0);
                // 获取不到，等待其他任务结束
                if (taskInfo == null)
                {
                    if (this._lock.SafeReadValue(() => this._workCount) > 0)
                    {
                        Thread.Sleep(1000);
                        this._sh.Release();
                        continue;
                    }
                    break;
                }
                taskInfo.Status = 1;
                this._collectTaskDal.Update(taskInfo);
                this._lock.SafeSetValue(count => this._workCount += count, 1);

                // 发起新线程，执行任务
                Task.Factory.StartNew((objTask) =>
                {                   
                    CollectTaskInvoker invoker = null;
                    var refTask = objTask as CollectTaskInfo;
                    try
                    {
                        invoker = this._lock.SafeReadValue(() =>
                        {
                            if (this._invokers.Count > 0)
                            {
                                return this._invokers.Dequeue();
                            }
                            return new CollectTaskInvoker();
                        });
                        invoker.Invoke(this._collect, refTask);
                        refTask.Status = 2;
                        this._collectTaskDal.Update(refTask);
                    }
                    catch
                    {
                        refTask.Status = 3;
                        this._collectTaskDal.Update(refTask);
                        throw;
                    }
                    finally
                    {
                        // 单个任务执行结束
                        if (invoker != null)
                        {
                            this._lock.SafeSetValue(t => this._invokers.Enqueue(t), invoker);
                        }
                        this._lock.SafeSetValue(count => this._workCount += count, -1);
                        this._sh.Release();
                    }
                }, taskInfo);
            }
        }      
        
        private void SetStopStatus()
        {
            this.Context.TaskStatus = CollectTaskStatus.Stop;
            this.OnStop?.Invoke(this, null);
        }

        private void ToRunningStatus()
        {
            this.Context.TaskStatus = CollectTaskStatus.Running;
            this.OnStart?.Invoke(this, null);
        }
    }
}
