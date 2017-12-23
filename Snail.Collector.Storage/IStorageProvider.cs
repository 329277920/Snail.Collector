
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Storage
{
    /// <summary>
    /// 存储模块提供程序接口
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// 新增一条或一组记录
        /// </summary>
        /// <param name="table">数据表名称</param>
        /// <param name="dataArray">需要插入的记录</param>
        /// <returns>返回新增条数</returns>
        int Insert<T>(string table, params T[] dataArray);

        /// <summary>
        /// 更新一条或一组记录
        /// </summary>
        /// <param name="table">数据表名称</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="dataArray">需要更新的记录</param>
        /// <returns>返回更新条数</returns>
        int Update<T>(string table, object filter, params T[] dataArray);

        /// <summary>
        /// 删除一条或一组记录
        /// </summary>
        /// <param name="table">数据表名称</param>     
        /// <param name="filter">过滤条件</param>
        /// <param name="dataArray">需要更新的记录</param>
        /// <returns>返回更新条数</returns>
        int Delete<T>(string table, object filter, params T[] dataArray);

        /// <summary>
        /// 查询一组记录
        /// </summary>
        /// <param name="table">数据表名称</param>
        /// <param name="filter">过滤条件</param>
        /// <returns>返回数据列表</returns>
        IEnumerable<T> Select<T>(string table, object filter);

        /// <summary>
        /// 查询一组记录
        /// </summary>
        /// <param name="table">数据表名称</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="top">查询条数</param>
        /// <returns>返回数据列表</returns>
        IEnumerable<T> Select<T>(string table, object filter, int top);

        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="table">数据表名称</param>
        /// <param name="filter">过滤条件</param>        
        /// <returns>返回数据列表</returns>
        T SelectSingle<T>(string table, object filter);
    }
}
