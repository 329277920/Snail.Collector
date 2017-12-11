using System;
using System.Collections.Generic;
using System.Text;

namespace Snail.Collector.Storage.DB
{
    /// <summary>
    /// 数据库存储配置
    /// </summary>
    public class DbProviderConfig
    {
        /// <summary>
        /// 提供者（mysql|sqlite|sqlserver）
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// 连接串
        /// </summary>
        public string Connection { get; set; }
    }
}
