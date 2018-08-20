using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Repositories
{
    /// <summary>
    /// 采集任务存储层
    /// </summary>
    public class CollectTaskRepository : ICollectTaskRepository
    {
        public void Insert(CollectTaskInfo taskInfo)
        {
            throw new NotImplementedException();
        }

        public CollectTaskInfo SelectSingle(int status)
        {
            throw new NotImplementedException();
        }

        public void Update(CollectTaskInfo taskInfo)
        {
            throw new NotImplementedException();
        }
    }
}
