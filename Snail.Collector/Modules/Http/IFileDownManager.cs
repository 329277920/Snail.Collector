using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Http
{
    /// <summary>
    /// 文件下载管理器接口
    /// </summary>
    public interface IFileDownManager
    {
        /// <summary>
        /// 并行下载文件
        /// </summary>
        /// <param name="loaders"></param>
        bool DownFiles(params FileDownloader[] loaders);
    }
}
