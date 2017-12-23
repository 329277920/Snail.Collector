using Newtonsoft.Json.Linq;
using Snail.Collector.Core.Storage.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Storage.DB.SqlParser
{
    /// <summary>
    /// 过滤sql解析器，应用于查询，修改，删除
    /// </summary>
    public class FilterParser
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
            return JsonParser.ToSqlQuery(model);
        }               
    }
}

