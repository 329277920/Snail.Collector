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
        /// 资源保存路径
        /// </summary>
        public string SourceSavePath { get; set; }

        /// <summary>
        /// 编码方式
        /// </summary>
        public Encoding Encode { get; set; }
    }
}
