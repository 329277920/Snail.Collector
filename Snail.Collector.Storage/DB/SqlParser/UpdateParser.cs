using Snail.Collector.Core.Storage.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Storage.DB.SqlParser
{
    /// <summary>
    /// 更新操作sql解析器
    /// </summary>
    public class UpdateParser
    {
        /// <summary>
        /// 根据模型对象解析出Sql语句
        /// </summary>
        /// <param name="table">数据表名称</param>
        /// <param name="model">模型对象</param>
        /// <param name="filters">需要过滤掉的字段名称</param>
        /// <returns>返回执行的sql</returns>
        public string Parse(string table, object model, params string[] filters)
        {
            var sql = new StringBuilder();
            sql.AppendFormat("UPDATE {0} SET ", table);
            var ps = JsonParser.GetProperties(model);
            if (ps == null || ps.Length <= 0)
            {
                return null;
            }         
            int i = 0;
            foreach (var name in ps.Select(item => item.Name))
            {
                if (filters?.Contains(name) == true)
                {
                    continue;
                }
                sql.AppendFormat("{0}{1}=@{1}", i++ > 0 ? "," : "", name);
            }
            return sql.ToString();
        }
    }
}
