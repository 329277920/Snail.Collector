using Snail.Collector.Log;
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
        private ILogger _logger;
        private ScriptEngineFactory _engineFactory;

        public event EventHandler OnStart;

        public event EventHandler OnStop;


        /// <summary>
        /// 获取采集任务上下文
        /// </summary>
        protected CollectTaskContext Context { get; private set; }

        public CollectTaskFactory(
            ICollectRepository collectDal, 
            ICollectTaskRepository collectTaskDal,
            ILogger logger)
        {
            this._collectDal = collectDal;
            this._collectTaskDal = collectTaskDal;
            this._logger = logger;
            this.Context = new CollectTaskContext() { TaskStatus = CollectTaskStatus.None };
            this._engineFactory = new ScriptEngineFactory(20);
        }
       
        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="collectId">采集任务id</param>
        public void Start(int collectId)
        {         
            var collectInfo = this._collectDal.SelectSingle(collectId);
            if (collectInfo == null)
            {
                this._logger.Error($"未找到任务id为:{collectId}的采集任务，此次采集结束。");
                this.SetStopStatus();
            }           
        }

        private void SetStopStatus()
        {
            this.Context.TaskStatus = CollectTaskStatus.Stop;
            this.OnStop?.Invoke(this, null);
        }

        private void ToRunningStatus()
        {
            this.Context.TaskStatus = CollectTaskStatus.Running;
            this.OnStart?.Invoke(this, null);
        }
    }
}
