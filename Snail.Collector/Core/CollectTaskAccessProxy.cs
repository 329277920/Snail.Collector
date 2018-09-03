using Snail.Collector.Common;
using Snail.Collector.Model;
using Snail.Collector.Repositories;
using System;
using System.Threading;


namespace Snail.Collector.Core
{
    /// <summary>
    /// 对脚本引擎公开任务访问接口
    /// </summary>
    public class CollectTaskAccessProxy
    {
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private ICollectTaskRepository _taskRepository;
        public CollectTaskAccessProxy(ICollectTaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        /// <summary>
        /// 将任务暂停指定的毫秒数
        /// </summary>
        /// <param name="millisecond"></param>
        public void sleep(int millisecond)
        {
            Thread.Sleep(millisecond);
        }

        /// <summary>
        /// 指定当前任务执行异常，并结束执行
        /// </summary>
        /// <param name="message">异常消息</param>
        public void error(string message)
        {
            throw new Exception(message);
        }

        /// <summary>
        /// 创建新任务
        /// </summary>
        /// <param name="task">{ uri:xx,script:xx }</param>
        public void add(dynamic task)
        {
            var collect = CallContextManager.GetCollectInfo();
            if (collect == null)
            {
                throw new Exception("未能从当前线程上下文中获取对象 ollectInfo");
            }
            var parentTask = CallContextManager.GetCollectTaskInfo();
            var newTask = new CollectTaskInfo();
            newTask.CollectId = collect.Id;
            newTask.RetryCount = 0;
            newTask.ScriptFilePath = task.script;
            newTask.Status = CollectTaskStatus.None;
            newTask.Uri = task.uri;
            if (parentTask != null)
            {
                newTask.ParentId = parentTask.Id;
                newTask.Uri = new Uri(new Uri(parentTask.Uri), newTask.Uri).ToString();
            }
            this._taskRepository.Insert(newTask);
        }

        /// <summary>
        /// 获取当前任务地址
        /// </summary>
        public string uri => CallContextManager.GetCollectTaskInfo()?.Uri;
    }
}
