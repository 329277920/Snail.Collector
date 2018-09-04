using Newtonsoft.Json;
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
        private ICollectContentRepository _contentRepository;
        public CollectTaskAccessProxy(
            ICollectTaskRepository taskRepository,
            ICollectContentRepository contentRepository)
        {
            this._taskRepository = taskRepository;
            this._contentRepository = contentRepository;
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
            throw new CollectTaskInvokeException(message);
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
        /// 添加采集数据
        /// </summary>
        /// <param name="strContent"></param>
        public void content(string strContent)
        {           
            if (string.IsNullOrEmpty(strContent))
            {
                return;
            }
            var task = CallContextManager.GetCollectTaskInfo();
            if (task == null)
            {
                throw new Exception("未能从当前线程上下文中获取对象 CollectTaskInfo");
            }
            this._contentRepository.Insert(new CollectContentInfo()
            {
                CollectId = task.CollectId,
                CollectTaskId = task.Id,
                Content = strContent
            });
        }

        /// <summary>
        /// 获取当前任务地址
        /// </summary>
        public string uri => CallContextManager.GetCollectTaskInfo()?.Uri;

        /// <summary>
        /// 获取绝对地址
        /// </summary>
        /// <param name="relatively"></param>
        /// <param name="relativeUri"></param>
        /// <returns></returns>
        public string absoluteUri(string relativeUri)
        {
            var baseUri = this.uri;
            if (string.IsNullOrEmpty(baseUri))
            {
                return relativeUri;
            }
            return new Uri(new Uri(baseUri), relativeUri).ToString();
        }
    }
}
