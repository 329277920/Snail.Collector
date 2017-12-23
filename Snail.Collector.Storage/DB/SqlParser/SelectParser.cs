using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Storage.DB.SqlParser
{
    /// <summary>
    /// 查询操作sql解析器
    /// </summary>
    public class SelectParser
    {
        /// <summary>
        /// 根据模型对象解析出Sql语句
        /// </summary>
        /// <param name="table">数据表名称</param>
        /// <param name="top">查询条数</param>
        /// <param name="model">模型对象</param>
        /// <param name="filters">需要过滤掉的字段名称</param>
        /// <returns>返回执行的sql</returns>
        public string Parse(string table, int top = 0, object model = null, params string[] filters)
        {
            return string.Format("SELECT {0} * FROM {1} ", top > 0 ? "TOP " + top : "", table);
        }
    }
}
