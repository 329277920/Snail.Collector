using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Repositories
{
    /// <summary>
    /// 采集实体
    /// </summary>
    public class CollectInfo
    {
        /// <summary>
        /// 采集id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 采集名称
        /// </summary>
        public string Name { get; set; }    
        
        /// <summary>
        /// 脚本文件路径
        /// </summary>
        public string ScriptFilePath { get; set; }
    }
}
