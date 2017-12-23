using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务项配置信息
    /// </summary>
    public class TaskItemSetting
    {
        /// <summary>
        /// 获取或设置页面地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 获取或设置此页面对应的脚本文件路径
        /// </summary>
        public string ScriptFile { get; set; }    

        /// <summary>
        /// 获取或设置当前对应的任务存储执行者
        /// </summary>
        public TaskInvokerStorageEntity TaskInvokerInfo { get; set; }
    }
}
