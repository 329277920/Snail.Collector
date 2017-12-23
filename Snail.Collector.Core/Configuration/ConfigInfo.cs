using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core.Configuration
{
    public class ConfigInfo
    {
        /// <summary>
        /// 系统数据库路径
        /// </summary>
        [JsonProperty("db")]
        public string DatabaseFilePath { get; set; }

        /// <summary>
        /// 任务失败后的重试次数
        /// </summary>
        [JsonProperty("errRetry")]
        public int ErrorRetry { get; set; }
    }
}
