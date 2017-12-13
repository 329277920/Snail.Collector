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
        /// 获取脚本引擎最大数量T
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

        /// <summary>
        /// 任务存储配置
        /// </summary>
        public DbProviderConfig Storage { get; set; }

        /// <summary>
        /// 资源存储配置
        /// </summary>
        public TaskSetting_Resource Resource { get; set; }
    }

    public class TaskSetting_Resource
    {
        /// <summary>
        /// 资源保存路径
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// 文件名生成规则(1:截取网络文件名)
        /// </summary>
        public int GenerateName { get; set; }

        /// <summary>
        /// 文件路径生成规则(1:截取网络路径)
        /// </summary>
        public int GeneratePath { get; set; }

        /// <summary>
        /// 替换现有文件(0:不替换,1:替换)
        /// </summary>
        public int Replace { get; set; }
    }
}
