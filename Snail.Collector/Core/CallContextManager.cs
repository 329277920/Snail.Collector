using Snail.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 调用上下文管理器
    /// </summary>
    public class CallContextManager
    {
        private const string KEY_COLLECT_INFO = "KEY_COLLECT_INFO";

        private const string KEY_COLLECT_TASK_INFO = "KEY_COLLECT_TASK_INFO";

        /// <summary>
        /// 获取绑定到当前线程上下文的 CollectInfo
        /// </summary>
        /// <returns></returns>
        public static CollectInfo GetCollectInfo()
        {
            return CallContext.GetData(KEY_COLLECT_INFO) as CollectInfo;
        }

        /// <summary>
        /// 获取绑定到当前线程上下文的 CollectTaskInfo
        /// </summary>
        /// <returns></returns>
        public static CollectTaskInfo GetCollectTaskInfo()
        {
            return CallContext.GetData(KEY_COLLECT_TASK_INFO) as CollectTaskInfo;
        }

        /// <summary>
        /// 设置绑定到当前线程上下文的 CollectInfo
        /// </summary>
        /// <returns></returns>
        public static void SetCollectInfo(CollectInfo collectInfo)
        {
            CallContext.SetData(KEY_COLLECT_INFO, collectInfo);
        }

        /// <summary>
        /// 设置绑定到当前线程上下文的 CollectTaskInfo
        /// </summary>
        /// <returns></returns>
        public static void SetCollectTaskInfo(CollectTaskInfo collectTaskInfo)
        {
            CallContext.SetData(KEY_COLLECT_TASK_INFO, collectTaskInfo);
        }
    }
}
