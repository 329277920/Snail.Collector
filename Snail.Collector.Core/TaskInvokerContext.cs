﻿using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务执行者上下文
    /// </summary>
    public class TaskInvokerContext
    {
        /// <summary>
        /// 获取任务绑定的脚本引擎
        /// </summary>
        public V8ScriptEngine Engine { get; internal set; }

        /// <summary>
        /// 获取或设置当前任务存储对象
        /// </summary>
        public TaskItemEntity TaskInvokerInfo { get; set; }

        /// <summary>
        /// 获取当前执行的任务对象
        /// </summary>
        public Task Task { get; internal set; }

        /// <summary>
        /// 获取任务执行的主目录
        /// </summary>
        public string ExecutePath { get; internal set; }

        public static TaskInvokerContext Current
        {
            get
            {
                return ContextManager.GetTaskInvokerContext();
            }
        }
    }
}
