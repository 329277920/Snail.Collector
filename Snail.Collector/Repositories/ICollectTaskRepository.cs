using Snail.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Repositories
{
    /// <summary>
    /// 任务存储层接口
    /// </summary>
    public interface ICollectTaskRepository
    {
        /// <summary>
        /// 插入一条采集任务
        /// </summary>
        /// <param name="taskInfo"></param>
        int Insert(CollectTaskInfo taskInfo);

        /// <summary>
        /// 更新采集任务
        /// </summary>
        /// <param name="taskInfo"></param>
        int Update(CollectTaskInfo taskInfo);

        /// <summary>
        /// 将某状态的任务全部修改为新的状态
        /// </summary>
        /// <param name="collectId"></param>
        /// <param name="oldStatus"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        int Update(int collectId,CollectTaskStatus oldStatus, CollectTaskStatus newStatus);

        /// <summary>
        /// 获取一条未采集的任务
        /// </summary>
        /// <param name="collectId">采集任务id</param>
        /// <param name="status">采集任务状态</param>
        /// <returns></returns>
        CollectTaskInfo SelectSingle(int collectId, CollectTaskStatus status);        
    }
}
