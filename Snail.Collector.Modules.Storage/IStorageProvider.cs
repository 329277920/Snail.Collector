using Snail.Collector.Modules.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Storage
{
    /// <summary>
    /// 存储模块提供程序接口
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// 将单条数据导出到外部存储
        /// </summary>
        /// <param name="config">配置信息</param>      
        /// <param name="data">导出数据项</param>
        /// <returns>返回导出数据数</returns>
        Task<int> ExportSingle(dynamic config, object data);

        /// <summary>
        /// 将数据集合导出到外部存储
        /// </summary>
        /// <param name="config">配置信息</param>      
        /// <param name="dataArray">导出数据项列表</param>
        /// <returns>返回导出数据数</returns>
        Task<int> Export(dynamic config, JSArray dataArray);
    }
}
