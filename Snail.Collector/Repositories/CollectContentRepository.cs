using Snail.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Repositories
{
    public class CollectContentRepository : ICollectContentRepository
    {
        public int Insert(CollectContentInfo contentInfo)
        {
            var sqlBuffer = new StringBuilder();
            sqlBuffer.Append("INSERT INTO CollectContents (CollectId,CollectTaskId,Content) ");
            sqlBuffer.Append($"SELECT {contentInfo.CollectId},{contentInfo.CollectTaskId},'{contentInfo.Content}' ");
            sqlBuffer.Append($"WHERE NOT EXISTS (SELECT * FROM CollectContents WHERE CollectId = {contentInfo.CollectId} AND CollectTaskId = {contentInfo.CollectTaskId})");
            return SqliteProxy.Execute(sqlBuffer.ToString());
        }
    }
}
