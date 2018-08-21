using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snail.Collector.Common.Sync
{
    /// <summary>
    /// 进程同步对象
    /// </summary>
    public sealed class ProcessSync
    {
        /// <summary>
        /// 在多个进程间，依次执行某一个方法
        /// </summary>
        /// <typeparam name="TResult">方法返回类型</typeparam>
        /// <param name="mutexName">同步对象名称</param>
        /// <param name="method">方法委托</param>
        /// <returns>返回执行结果</returns>
        public static TResult Invoke<TResult>(string mutexName, Func<TResult> method)
        {
            return InnerInvoke(mutexName, () => 
            {
                return method();
            });
        }

        private static TResult InnerInvoke<TResult>(string mutexName, ProcessSyncHandle<TResult> method)
        {
            Mutex mutex = null;
            try
            {
                mutex = new Mutex(false, mutexName);
                mutex.WaitOne();
                return method();
            }
            finally
            {
                if (mutex != null)
                {
                    mutex.ReleaseMutex();
                }
            }
        }
        private delegate TResult ProcessSyncHandle<TResult>();
    }
}
