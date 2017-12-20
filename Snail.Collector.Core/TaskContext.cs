using Microsoft.ClearScript.V8;
using Snail.Collector.Core.SystemModules;
using Snail.Collector.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务执行环境上下文  
    /// </summary>
    public class TaskContext
    {
        internal TaskContext()
        {
            this.Stat = new TaskStatictics();
        }

        /// <summary>
        /// 获取任务执行的工作目录
        /// </summary>
        public string ExecutePath { get; internal set; }     

        /// <summary>
        /// 获取当前任务ID
        /// </summary>
        public int TaskId { get; internal set; }

        /// <summary>
        /// 获取当前执行任务的V8引擎
        /// </summary>
        public V8ScriptEngine Engine { get; internal set; }         

        /// <summary>
        /// 任务执行统计信息
        /// </summary>
        public TaskStatictics Stat { get; private set; }

        /// <summary>
        /// 将此上线文绑定到线程执行环境
        /// </summary>
        internal void BindContext()
        {
            ContextManager.SetTaskContext(this);
        }

        /// <summary>
        /// 获取当前任务上下文
        /// </summary>
        public static TaskContext Current
        {
            get
            {
                return ContextManager.GetTaskContext();
            }
        }        

        /// <summary>
        /// 任务配置
        /// </summary>
        public TaskSetting Settings { get; set; }

        internal void ClearStat()
        {
            this.Stat = new TaskStatictics()
            {
                StartTime = DateTime.Now
            };
        }
    }
}
