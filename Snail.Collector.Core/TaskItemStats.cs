using Snail.Collector.Core.Configuration;
using Snail.Collector.Storage;
using Snail.Collector.Storage.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务执行者存储类
    /// </summary>
    public class TaskItemStats
    {
        public static TaskItemStats Instance = new Lazy<TaskItemStats>(() =>
        {
            return new TaskItemStats(new DbProviderConfig
            {
                Driver = StorageProviders.Sqlite,
                Connection = "Data Source=" + ConfigManager.Current.DatabaseFilePath + ";Version=3;"
            });
        }, true).Value;

        private IStorageProvider _db;

        private string _tbName = "TaskItemStats";

        private TaskItemStats(DbProviderConfig cfg)
        {
            this._db = new DbProvider(cfg);
        }

        // public void 
    }
}
