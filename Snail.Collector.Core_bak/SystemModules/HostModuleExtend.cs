
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Snail.Collector.Core.Configuration;
using Snail.Collector.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Snail.Collector.Core.SystemModules
{
    public class HostModuleExtend : ExtendedHostFunctions
    {        
        /// <summary>
        /// 加载指定的模块到JS运行环境中
        /// </summary>
        /// <param name="module">模块名称</param>        
        public object require(string module)
        {
            return GetEngine()?.LoadModule(module);         
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
                    // todo: 写入日志
                    return;
                }
                if (TaskInvokerStorage.Instance.AddObj(new
                {
                    taskId = invokerContext.TaskContext.TaskId,
                    parentId = invokerContext.TaskInvokerInfo?.Id,
                    url = url,
                    script = script
                }))
                {
                    // todo: 写入日志
                };
            }
            catch (Exception ex)
            {
                // 写入日志

            }
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
