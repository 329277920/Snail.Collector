using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Storage
{
    /// <summary>
    /// 储存模块
    /// </summary>
    public class StorageModule
    {
        public IStorageProvider provider(string name)
        {
            switch (name)
            {
                case "db":
                    return new DbProvider();
            }
            return null;
        }
    }
}
