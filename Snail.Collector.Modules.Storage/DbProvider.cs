using Dapper;
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
        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="config">数据库配置{ provider:"",conString:"",table:"" }</param>
        /// <param name="callBack">回调</param>
        /// <param name="items">实体集合</param>
        public async Task import(dynamic config, params object[] items)
        {             
            if (items == null || items.Length <= 0)
            {
                return;
            }
            var rest = 0;
            try
            {
                string sql = getInsertString(config.table, (IDynamicMetaObjectProvider)items[0]);

                using (IDbConnection db = getConnection(config.provider, config.conString))
                {
                    rest = await db.ExecuteAsync(sql, from item in items
                                                      select new StorageEntity(item));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                // var item = ex.ToString();
            }
            // callBack?.Invoke(rest);
        }

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
    }
}
