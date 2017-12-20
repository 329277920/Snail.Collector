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

        public void config(dynamic cfg)
        {
            try
            {
                base.Config(new DbProviderConfig() { Driver = cfg.provider, Connection = cfg.connectionString });
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, "call config error.", ex);
            }
        }

        public bool add(string table, params object[] data)
        {
            try
            {                                
                var rest = base.insert(table, data);
                var invokerContext = ContextManager.GetTaskInvokerContext();
                if (rest > 0 && invokerContext != null && invokerContext.Task != null)
                {
                    invokerContext.Task.SetStat(rest, TaskStatTypes.Article);
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
