using Snail.Collector.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Repositories
{
    /// <summary>
    /// 采集任务存储层
    /// </summary>
    public class CollectTaskRepository : ICollectTaskRepository
    {
        public int Insert(CollectTaskInfo taskInfo)
        {
            var sqlBuffer = new StringBuilder();
            sqlBuffer.Append("INSERT INTO CollectTasks (ParentId,CollectId,Uri,ScriptFilePath,Status,RetryCount) ");
            sqlBuffer.Append($"SELECT {taskInfo.ParentId},{taskInfo.CollectId},'{taskInfo.Uri}','{taskInfo.ScriptFilePath}',{(int)taskInfo.Status},{taskInfo.RetryCount} ");
            sqlBuffer.Append($"WHERE NOT EXISTS (SELECT * FROM CollectTasks WHERE Uri = '{taskInfo.Uri}')");
            return SqliteProxy.Execute(sqlBuffer.ToString());
        }

        public CollectTaskInfo SelectSingle(int collectId, CollectTaskStatus status)
        {
            var sql = $"SELECT * FROM CollectTasks WHERE CollectId = {collectId} AND Status = {(int)status}";
            return SqliteProxy.SelectSingle(sql, this.Convert);
        }

        public int Update(CollectTaskInfo taskInfo)
        {
            var sqlBuffer = new StringBuilder();
            sqlBuffer.Append("UPDATE CollectTasks SET ");
            sqlBuffer.Append($"Status = {(int)taskInfo.Status}, RetryCount = {taskInfo.RetryCount} ");
            sqlBuffer.Append($"WHERE Id = {taskInfo.Id}");
            return SqliteProxy.Execute(sqlBuffer.ToString());
        }

        public int Update(int collectId, CollectTaskStatus oldStatus, CollectTaskStatus newStatus)
        {
            var sql = $"UPDATE CollectTasks SET Status = {(int)newStatus} WHERE CollectId = {collectId} AND Status = {(int)oldStatus}";
            return SqliteProxy.Execute(sql);
        }

        private CollectTaskInfo Convert(IDataRecord dr)
        {
            return new CollectTaskInfo()
            {
                Id = int.Parse(dr["Id"].ToString()),
                CollectId = int.Parse(dr["CollectId"].ToString()),
                ParentId = int.Parse(dr["ParentId"].ToString()),
                RetryCount = int.Parse(dr["RetryCount"].ToString()),
                Status = (CollectTaskStatus)int.Parse(dr["Status"].ToString()),
                Uri = (string)dr["Uri"],
                ScriptFilePath = (string)dr["ScriptFilePath"]
            };
        }
    }
}
