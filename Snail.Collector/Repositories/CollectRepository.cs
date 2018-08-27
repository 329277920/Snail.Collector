using System;
using System.Collections.Generic;
using System.Data;
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
            var sql = $"SELECT * FROM Collects WHERE Id = {id}";
            return SqliteProxy.SelectSingle(sql, this.Convert);
        }

        private CollectInfo Convert(IDataRecord dr)
        {
            return new CollectInfo()
            {
                Id = int.Parse(dr["Id"].ToString()),
                Name = (string)dr["Name"],
                ScriptFilePath = (string)dr["ScriptFilePath"]
            };
        }
    }
}
