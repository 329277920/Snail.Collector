using Snail.Collector.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector
{
    /// <summary>
    /// 采集任务工厂
    /// </summary>
    public class CollectTaskFactory
    {
        private ICollectRepository _collectDal;
        private ICollectTaskRepository _collectTaskDal;

        /// <summary>
        /// 获取采集任务上下文
        /// </summary>
        protected CollectTaskContext Context { get; private set; }

        public CollectTaskFactory(ICollectRepository collectDal, ICollectTaskRepository collectTaskDal)
        {
            this._collectDal = collectDal;
            this._collectTaskDal = collectTaskDal;
            this.Context = new CollectTaskContext();
        }

        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="collectInfo">采集任务实体</param>
        public void Start(CollectInfo collectInfo)
        {
            // 校验是否新增任务
            var oldCollectInfo = this._collectDal.SelectSingle(collectInfo.Id);
            if (oldCollectInfo == null)
            {
                this._collectDal.Insert(new CollectInfo()
                {
                    Id = collectInfo.Id,
                    Name = collectInfo.Name
                });
            }
        }
    }
}
