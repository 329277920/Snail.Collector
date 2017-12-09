using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 执行上下文管理器
    /// </summary>
    internal sealed class ContextManager
    {
        private const string Key_TaskInvokerContext = "TaskInvokerContext_Data";

        private const string Key_TaskContext = "TaskContext_Data";

        /// <summary>
        /// 绑定任务执行上下文
        /// </summary>
        /// <param name="context"></param>
        public static void SetTaskInvokerContext(TaskInvokerContext context)
        {
            CallContext.SetData(Key_TaskInvokerContext, context);
        }

        /// <summary>
        /// 获取任务执行上下文
        /// </summary>      
        /// <returns></returns>
        public static TaskInvokerContext GetTaskInvokerContext()
        {
            return CallContext.GetData(Key_TaskInvokerContext) as TaskInvokerContext;
        }

        /// <summary>
        /// 绑定任务上下文
        /// </summary>
        /// <param name="context"></param>
        public static void SetTaskContext(TaskContext context)
        {
            CallContext.SetData(Key_TaskContext, context);
        }

        /// <summary>
        /// 获取任务上下文
        /// </summary>      
        /// <returns></returns>
        public static TaskContext GetTaskContext()
        {
            return CallContext.GetData(Key_TaskContext) as TaskContext;
        }
    }
}
