using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务统计类型
    /// </summary>
    public enum TaskStatTypes
    {
        Task = 0,
        ErrTask = 1,
        NewTask = 2,
        File = 3,
        Article = 4
    }
}
