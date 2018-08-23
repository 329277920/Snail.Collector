using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Repositories
{
    public class CollectRepository : ICollectRepository
    {
        public int Insert(CollectInfo collectInfo)
        {
            var sql = $"INSERT INTO Collects VALUES('{collectInfo.Id}','{collectInfo.Name}','{collectInfo.ScriptFilePath}')";
            return SqliteProxy.Execute(sql);
        }

        public CollectInfo SelectSingle(int id)
        {
            throw new NotImplementedException();
        }
    }
}
