using Snail.Collector.Common;
using Snail.Collector.Storage;
using Snail.Collector.Storage.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core.SystemModules
{
    /// <summary>
    /// 存储模块
    /// </summary>
    public class StorageDataModuleExtend : StorageDataModule
    {
        protected string LogSource = "storage";

        public StorageDataModuleExtend(DbProviderConfig cfg)
        {
            base.Config(cfg);
        }

        public bool add(string table, params object[] data)
        {
            try
            {
                var invokerContext = ContextManager.GetTaskInvokerContext();
                if (invokerContext == null)
                {
                    throw new Exception("failed to get the taskInvokerContext.");
                }
                var rest = base.insert(table, data);
                if (rest > 0)
                {
                    invokerContext.TaskContext.SetStat(rest, TaskStatTypes.Article);
                }
                return rest > 0;
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, string.Format("call add error,table is {0}.", table), ex);
            }
            return false;
        }        
    }
}
