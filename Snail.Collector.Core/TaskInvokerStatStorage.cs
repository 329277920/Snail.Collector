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
    public class TaskInvokerStatStorage
    {
        public static TaskInvokerStatStorage Instance = new Lazy<TaskInvokerStatStorage>(() =>
        {
            return new TaskInvokerStatStorage(new
            {
                driver = StorageProviders.Sqlite,
                conStr = "Data Source=" + ConfigManager.Current.DatabaseFilePath + ";Version=3;"
            });
        }, true).Value;

        private IStorageProvider _db;

        private string _tbName = "TaskInvokerStats";

        private TaskInvokerStatStorage(dynamic cfg)
        {
            this._db = new DbProvider(new DbProviderConfig() { Provider = cfg.driver, ConnectionString = cfg.conStr });
        }

        public void 
    }
}
