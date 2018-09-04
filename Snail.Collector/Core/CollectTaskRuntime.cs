using Snail.Collector.Core;
using Snail.Collector.Model;
using Snail.Collector.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using Snail.Collector.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Snail.Collector
{
    /// <summary>
    /// 采集任务工厂
    /// </summary>
    public class CollectTaskRuntime : IDisposable
    {
        private ICollectRepository _collectDal;
        private ICollectTaskRepository _collectTaskDal;
        private ILogger _logger;
        private Queue<CollectTaskInvoker> _invokers;
        private Semaphore _sh;
        private ReaderWriterLockSlim _lock;
        private CollectInfo _collect;         
        private IConfiguration _config;
        private int _retry = 0;

        /// <summary>
        /// 当某个任务执行完成后触发
        /// </summary>
        public event EventHandler<CollectTaskInvokeCompleteArgs> OnCollectTaskInvokeComplete;

        /// <summary>
        /// 获取统计信息
        /// </summary>
        public CollectTaskState State { get; private set; }


        public CollectTaskRuntime(
            ICollectRepository collectDal, 
            ICollectTaskRepository collectTaskDal,
            ILogger logger,
            IConfiguration config)
        {
            this._config = config;
            this._collectDal = collectDal;
            this._collectTaskDal = collectTaskDal;
            this._logger = logger;
            this._sh = new Semaphore(int.Parse(this._config["task:parallelTasks"]), int.Parse(this._config["task:parallelTasks"]));
            this._invokers = new Queue<CollectTaskInvoker>();
            this._lock = new ReaderWriterLockSlim();            
            this._retry = int.Parse(this._config["task:errorRepeat"]);
            this.State = new CollectTaskState();
        }

        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="collectInfo"></param>
        public void Start(CancellationToken cancellationToken, CollectInfo collectInfo)
        {
            this._collect = collectInfo;

            // 重置任务状态
            this._collectTaskDal.Update(collectInfo.Id, CollectTaskStatus.Running, CollectTaskStatus.None);
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            // 执行初始化脚本，不做异常捕获，发生异常直接退出
            using (var initInvoker = new CollectTaskInvoker(this.State))
            {
                initInvoker.Invoke(collectInfo, null);
            }
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            // 循环执行
            while (true)
            {
                // 取消执行，等待其他任务结束
                if (cancellationToken.IsCancellationRequested)
                {
                    if (this.State.RunningTaskCount > 0)
                    {
                        Thread.Sleep(500);
                        continue;
                    }
                    return;
                }

                // 请求执行
                this._sh.WaitOne();
                var taskInfo = this._collectTaskDal.SelectSingle(collectInfo.Id, CollectTaskStatus.None);
                if (taskInfo == null)
                {
                    taskInfo = this._collectTaskDal.SelectSingle(collectInfo.Id, CollectTaskStatus.Error);
                }
                // 获取不到，等待其他任务结束
                if (taskInfo == null)
                {
                    if (this.State.RunningTaskCount > 0)
                    {
                        Thread.Sleep(500);
                        this._sh.Release();
                        continue;
                    }
                    return;
                }
                taskInfo.Status = CollectTaskStatus.Running;
                taskInfo.RetryCount++;
                this._collectTaskDal.Update(taskInfo);
                this.State.RunningTaskCount = 1;                 

                // 发起新线程，执行任务
                Task.Factory.StartNew((objTask) =>
                {
                    
                    CollectTaskInvoker invoker = null;
                    var refTask = objTask as CollectTaskInfo;
                    var invokeArgs = new CollectTaskInvokeCompleteArgs() { Task = refTask };
                    try
                    {
                        invoker = this._lock.SafeReadValue(() =>
                        {
                            if (this._invokers.Count > 0)
                            {
                                return this._invokers.Dequeue();
                            }
                            return new CollectTaskInvoker(this.State); 
                        });
                        invoker.Invoke(this._collect, refTask);
                        refTask.Status =  CollectTaskStatus.Complete;
                        this._collectTaskDal.Update(refTask);
                        invokeArgs.Success = true;
                    }
                    catch(Exception ex)
                    {
                        refTask.Status =  CollectTaskStatus.Error;                       
                        if (refTask.RetryCount >= this._retry)
                        {
                            refTask.Status = CollectTaskStatus.Faild;
                        }
                        this._collectTaskDal.Update(refTask);
                        invokeArgs.Success = false;
                        invokeArgs.Error = ex;
                    }
                    finally
                    {
                        // 单个任务执行结束
                        if (invoker != null)
                        {
                            this._lock.SafeSetValue(t => this._invokers.Enqueue(t), invoker);
                        }
                        if (invokeArgs.Success)
                        {
                            this.State.CompleteTaskCount = 1;
                        }
                        else
                        {
                            this.State.ErrorTaskCount = 1;
                        }
                        this.OnCollectTaskInvokeComplete?.Invoke(this, invokeArgs);                        
                        this.State.RunningTaskCount = -1;
                        this._sh.Release();
                    }
                }, taskInfo);
            }
        }

        #region 资源释放

        private bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;         
            if (disposing)
            {
                if (this._invokers != null)
                {
                    while (this._invokers.Count > 0)
                    {
                        this._invokers.Dequeue().Dispose();
                    }
                }
                if (this._sh != null)
                {
                    this._sh.Dispose();
                }
                if (this._lock != null)
                {
                    this._lock.Dispose();
                }                
            }
        }
        #endregion
    }
}
