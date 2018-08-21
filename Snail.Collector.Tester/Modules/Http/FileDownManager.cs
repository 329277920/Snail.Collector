using Snail.Collector.Common.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snail.Collector.Modules.Http
{
    /// <summary>
    /// 文件下载管理器，控制下载并发
    /// </summary>
    internal sealed class FileDownManager
    {
        private static Parallel Parallel;

        private static object LckObj = new object();

        private const int DefMax = 20;

        /// <summary>
        /// 初始化下载器工厂
        /// </summary>
        /// <param name="max"></param>
        public static void Init(int max)
        {
            if (Parallel != null)
            {
                return;
            }
            lock (LckObj)
            {
                if (Parallel != null)
                {
                    return;
                }
                Parallel = new Parallel(max);
            }           
        }

        /// <summary>
        /// 并行下载文件
        /// </summary>
        /// <param name="loaders"></param>
        public static bool DownFiles(params FileDownloader[] loaders)
        {
            if (loaders == null || loaders.Length <= 0)
            {
                return true;
            }
            Init(DefMax);
            var rest = true;
            Parallel.ForEach(loaders, loader => 
            {
                if (!loader.Down())
                {
                    rest = false;
                }
            });
            return rest;
        }
    }
}
