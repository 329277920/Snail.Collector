using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snail.Collector.Common.Sync
{
    /// <summary>
    /// 线性执行项
    /// </summary>
    public class ParallelForeachItem<TSource>
    {
        /// <summary>
        /// 线性执行的自定义对象
        /// </summary>
        public TSource Item { get; set; }

        /// <summary>
        /// 获取对象是否成功，将结束线性执行过程
        /// </summary>
        public bool Success { get; set; }
    }
}
