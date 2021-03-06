﻿using System;
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
        void Insert(CollectTaskInfo taskInfo);

        /// <summary>
        /// 更新采集任务
        /// </summary>
        /// <param name="taskInfo"></param>
        void Update(CollectTaskInfo taskInfo);

        /// <summary>
        /// 获取一条未采集的任务
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        CollectTaskInfo SelectSingle(int status);        
    }
}
