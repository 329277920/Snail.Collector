using Microsoft.Extensions.Configuration;
using Snail.Collector.Common;
using Snail.Collector.Common.Sync;
using System;

namespace Snail.Collector.Modules.Http
{
    /// <summary>
    /// 文件下载管理器，控制下载并发
    /// </summary>
    public class FileDownManager : IFileDownManager
    {
        private Parallel _parallel;
     
        private ILogger _logger;

        /// <summary>
        /// 初始化文件下载器
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        public FileDownManager(ILogger logger, IConfiguration config)
        {
            this._parallel = new Parallel(int.Parse(config["http:parallelDownloads"]));
            this._logger = logger;
        }
         
        /// <summary>
        /// 并行下载文件
        /// </summary>
        /// <param name="loaders"></param>
        public bool DownFiles(params FileDownloader[] loaders)
        {
            if (loaders == null || loaders.Length <= 0)
            {
                return true;
            }           
            var rest = true;
            this._parallel.ForEach(loaders, loader => 
            {
                try
                {
                    if (!loader.Down())
                    {
                        rest = false;
                    }
                }
                catch (Exception ex)
                {
                    rest = false;
                    this._logger.Error($"下载文件失败,'{loader.Uri}'", ex);
                }                
            });
            return rest;
        }
    }
}
