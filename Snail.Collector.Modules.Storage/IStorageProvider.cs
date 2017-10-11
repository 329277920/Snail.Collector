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
        Task import(dynamic config, params object[] items);
    }
}
