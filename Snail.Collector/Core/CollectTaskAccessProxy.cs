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
            throw new CollectTaskInvokeException("UserError:" + message);
        }

        /// <summary>
        /// 创建新任务
        /// </summary>
        /// <param name="task">{ uri:xx,script:xx }</param>
        public int add(dynamic task)
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
            return this._taskRepository.Insert(newTask);
        }

        /// <summary>
        /// 添加采集数据
        /// </summary>
        /// <param name="strContent"></param>
        public int content(string strContent)
        {
            if (string.IsNullOrEmpty(strContent))
            {
                return -1;
            }
            var task = CallContextManager.GetCollectTaskInfo();
            if (task == null)
            {
                throw new Exception("未能从当前线程上下文中获取对象 CollectTaskInfo");
            }
            return this._contentRepository.Insert(new CollectContentInfo()
            {
                CollectId = task.CollectId,
                CollectTaskId = task.Id,
                Content = strContent
            });
        }

        /// <summary>
        /// 获取一个新的文件名，格式({collectId_collectTaskId_fileName})
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public string newFileName(string uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                return "";
            }
            var idx = uri.LastIndexOf("/");
            if (idx >= 0)
            {
                uri = uri.Substring(idx + 1);
            }
            var collect = CallContextManager.GetCollectInfo();
            if (collect == null)
            {
                throw new Exception("未能从当前线程上下文中获取对象 ollectInfo");
            }
            var task = CallContextManager.GetCollectTaskInfo();
            return $"{collect.Id}_{(task == null ? 0 : task.Id)}_{uri}";
        }

        private string _uri;
        /// <summary>
        /// 获取或设置当前任务地址
        /// </summary>
        public string uri {
            get {
                if (this._uri == null)
                {
                    return CallContextManager.GetCollectTaskInfo()?.Uri;
                }
                return this._uri;
            }
            set {
                this._uri = value;
            }
        }
                      
        /// <summary>
        /// 获取绝对地址
        /// </summary>      
        /// <param name="relativeUri"></param>
        /// <param name="baseUri"></param>
        /// <returns></returns>
        public string absoluteUri(string relativeUri, string baseUri = "")
        {
            if (string.IsNullOrEmpty(baseUri))
            {
                baseUri = this.uri;
            }         
            if (string.IsNullOrEmpty(baseUri))
            {
                return relativeUri;
            }
            return new Uri(new Uri(baseUri), relativeUri).ToString();
        }
    }
}
