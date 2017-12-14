
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Snail.Collector.Common;
using Snail.Collector.Core.Configuration;
using Snail.Collector.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Snail.Collector.Core.SystemModules
{
    public class HostModuleExtend : ExtendedHostFunctions
    {
        private const string LogSource = "host";

        /// <summary>
        /// 加载指定的模块到JS运行环境中
        /// </summary>
        /// <param name="module">模块名称</param>        
        public object require(string module)
        {
            try
            {
                return GetEngine()?.LoadModule(module);
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, string.Format("call require error.module name is '{0}'.", module), ex);
            }
            return null;
        }

        /// <summary>
        /// 写入调试信息
        /// </summary>
        /// <param name="value"></param>
        public void debug(object value)
        {
            System.Diagnostics.Debug.WriteLine(value);
        }

        /// <summary>
        /// 将当前线程暂停指定毫秒数
        /// </summary>
        /// <param name="millisecondsTimeout"></param>
        public void sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }

        /// <summary>
        /// 新增一个任务
        /// </summary>
        /// <param name="url">任务地址</param>
        /// <param name="script">脚本文件名称</param>
        public void newTask(string url, string script)
        {
            try
            {
                var invokerContext = ContextManager.GetTaskInvokerContext();
                if (invokerContext == null)
                {
                    throw new Exception("failed to get the taskInvokerContext.");
                }
                if (!TaskItems.Instance.AddObj(new
                {
                    taskId = invokerContext.TaskContext.TaskId,
                    parentId = invokerContext.TaskInvokerInfo?.Id,
                    url = url,
                    script = script
                }))
                {
                    LoggerProxy.Error(LogSource, string.Format("call newTask error, add task failed. url is '{0}',script is '{1}'.", url, script));
                }
                else
                {
                    invokerContext.TaskContext.SetStat(1, TaskStatTypes.NewTask);
                }                
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, string.Format("call newTask error. url is '{0}',script is '{1}'.", url, script), ex);
            }
        }

        /// <summary>
        /// 获取一个完整路径
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="baseUri"></param>
        /// <returns></returns>
        public string getUri(string uri, string baseUri)
        {
            try
            {
                if (string.IsNullOrEmpty(baseUri))
                {
                    baseUri = TaskInvokerContext.Current?.TaskInvokerInfo?.Url;
                }
                if (string.IsNullOrEmpty(baseUri))
                {
                    return uri;
                }
                return new Uri(new Uri(baseUri), uri).ToString();
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, string.Format("call getUri error. url is '{0}',baseUri is '{1}'.", uri, baseUri), ex);
            }
            return "";
        }

        public TaskInvokerContext taskContext
        {
            get { return TaskInvokerContext.Current; }
        }

        #region 私有成员

        private V8ScriptEngine GetEngine()
        {
            return this.GetTaskContext()?.Engine;           
        }

        private TaskContext GetTaskContext()
        {
            var task = ContextManager.GetTaskInvokerContext()?.TaskContext;
            if (task == null)
            {
                return ContextManager.GetTaskContext();
            }
            return task;
        }
        
        #endregion
    }
}
