using Snail.Collector.Core.Storage.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Storage.DB.SqlParser
{
    /// <summary>
    /// 新增操作sql解析器
    /// </summary>
    public class InsertParser
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
            var ps = JsonParser.GetProperties(model);
            if (ps == null || ps.Length <= 0)
            {
                return null;
            }            
            int i = 0;
            var sqlPs = new StringBuilder();
            var sqlVs = new StringBuilder(); 
            foreach (var name in ps.Select(item => item.Name))
            {
                if (filters?.Contains(name) == true)
                {
                    continue;
                }
                var split = i++ > 0 ? "," : "";
                sqlPs.AppendFormat("{0}{1}", split, name);
                sqlVs.AppendFormat("{0}@{1}", split, name);
            }
            return string.Format("INSERT INTO {0}({1}) VALUES({2})", table, sqlPs, sqlVs);
        }

        //动态类型
        //private string getInsertString(string tableName, IDynamicMetaObjectProvider value)
        //{
        //    StringBuilder sqlPs = new StringBuilder("");
        //    StringBuilder sqlVs = new StringBuilder("");

        //    var properties = value.GetMetaObject(Expression.Constant(value)).GetDynamicMemberNames();
        //    int len = properties.Count();
        //    int i = 0;
        //    foreach (var name in properties)
        //    {
        //        var split = i++ < len - 1 ? "," : "";
        //        sqlPs.AppendFormat("{0}{1}", name, split);
        //        sqlVs.AppendFormat("@{0}{1}", name, split);
        //    }
        //    return string.Format("INSERT INTO {0}({1}) VALUES({2})", tableName, sqlPs, sqlVs);
        //}
    }
}
