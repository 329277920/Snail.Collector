using Snail.Collector.Storage.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 主任务配置信息
    /// </summary>
    public class TaskSetting
    {
        /// <summary>
        /// 获取任务ID
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 获取获设置任务的友好名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 获取脚本引擎最大数量
        /// </summary>
        public int Parallel { get; set; }

        /// <summary>
        /// 任务开始地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 任务开始脚本文件路径
        /// </summary>
        public string Script { get; set; }        
    }    
}
