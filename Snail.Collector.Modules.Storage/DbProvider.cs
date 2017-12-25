using Dapper;
using Snail.Collector.Modules.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Storage
{
    /// <summary>
    /// 数据库存储
    /// </summary>
    public class DbProvider : IStorageProvider
    {
        #region 内部方法

        /// 将数据导出到外部存储
        /// </summary>
        /// <param name="config">配置信息</param>       
        /// <param name="data">导出数据项</param>
        /// <returns></returns>
        public async Task<int> ExportSingle(dynamic config, object data)
        {
            if (data == null)
            {
                return 0;
            }
            string sql = getInsertString(config.table, (IDynamicMetaObjectProvider)data);

            using (IDbConnection db = getConnection(config.target, config.conString))
            {
                return await db.ExecuteAsync(sql, new StorageEntity(data));
            }
        }

        /// <summary>
        /// 将数据集合导出到外部存储
        /// </summary>
        /// <param name="config">配置信息</param>      
        /// <param name="dataArray">导出数据项列表</param>
        /// <returns>返回导出数据数</returns>
        public async Task<int> Export(dynamic config, JSArray dataArray)
        {
            if (dataArray == null || dataArray.Count <= 0)
            {
                return 0;
            }
            string sql = getInsertString(config.table, (IDynamicMetaObjectProvider)dataArray[0]);

            using (IDbConnection db = getConnection(config.target, config.conString))
            {
                return await db.ExecuteAsync(sql, from item in dataArray
                                                  select new StorageEntity(item));
            }
        }

        #endregion


        #region 私有方法

        private string getInsertString(string tableName, IDynamicMetaObjectProvider value)
        {
            StringBuilder sqlPs = new StringBuilder("");
            StringBuilder sqlVs = new StringBuilder("");

            var properties = value.GetMetaObject(Expression.Constant(value)).GetDynamicMemberNames();
            int len = properties.Count();
            int i = 0;
            foreach (var name in properties)
            {
                var split = i++ < len - 1 ? "," : "";
                sqlPs.AppendFormat("{0}{1}", name, split);
                sqlVs.AppendFormat("@{0}{1}", name, split);
            }
            return string.Format("INSERT INTO {0}({1}) VALUES({2})", tableName, sqlPs, sqlVs);
        }

        public void Insert(dynamic config, params object[] items)
        {

        }

        private IDbConnection getConnection(string provider, string conStr)
        {
            switch (provider)
            {
                case "mysql":
                    return new MySql.Data.MySqlClient.MySqlConnection(conStr);
            }
            return null;
        }

        #endregion
    }
}
