using Dapper;
using Snail.Collector.Core.Storage.DB;
using Snail.Collector.Storage.DB.SqlParser;
using Snail.Collector.Storage.DB.StorageModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Snail.Collector.Storage.DB
{
    /// <summary>
    /// 数据库存储
    /// </summary>
    public class DbProvider : IStorageProvider
    {
        /// <summary>
        /// 数据库连接串
        /// </summary>
        private string _conStr;

        /// <summary>
        /// 存储目标介质(mysql,sqlserver)
        /// </summary>
        private string _driver;

        public DbProvider(DbProviderConfig config)
        {
            this._conStr = config.ConnectionString;
            this._driver = config.Provider;
        }

        /// <summary>
        /// 新增一条或一组记录
        /// </summary>
        /// <param name="table">数据表名称</param>
        /// <param name="dataArray">需要插入的记录</param>
        /// <returns>返回新增条数</returns>
        public int Insert<T>(string table, params T[] dataArray)
        {
            if (dataArray == null || dataArray.Length <= 0)
            {
                return 0;
            }
            var entities = JsonParser.ConvertEntities(dataArray);
            var sql = new InsertParser().Parse(table, entities[0]);
            return this.Execute(sql, entities.Select((source) =>
            {
                return new InsertModel(source);
            }).ToArray());
        }

        /// <summary>
        /// 更新一条或一组记录
        /// </summary>
        /// <param name="table">数据表名称</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="dataArray">需要更新的记录</param>
        /// <returns>返回更新条数</returns>
        public int Update<T>(string table, object filter, params T[] dataArray)
        {
            if (dataArray == null || dataArray.Length <= 0)
            {
                return 0;
            }
            var filterEntities = JsonParser.ConvertEntities(filter);
            if (filterEntities.Count <= 0)
            {
                throw new Exception("未传入筛选条件,未防止错误,该执行已停止.");
            }
            var filterSql = new FilterParser().Parse(table, filterEntities[0]);
            if (string.IsNullOrEmpty(filterSql))
            {
                throw new Exception("未能解析出需要筛选sql语句,未防止错误,该执行已停止.");
            }
            var entities = JsonParser.ConvertEntities(dataArray);
            var filterNames = JsonParser.GetProperties(filterEntities[0]).Select(item => item.Name);
            var sql = new UpdateParser().Parse(table, entities[0], filterNames.ToArray());
            if (string.IsNullOrEmpty(sql))
            {
                throw new Exception("未能解析出需要执行的sql语句.");
            }
            return this.Execute(sql + " WHERE " + filterSql, entities.Select((source) =>
            {
                return new UpdateModel(source, filterEntities[0]);
            }).ToArray());
        }

        /// <summary>
        /// 删除一条或一组记录
        /// </summary>
        /// <param name="table">数据表名称</param>     
        /// <param name="filter">过滤条件</param>
        /// <param name="dataArray">需要更新的记录</param>
        /// <returns>返回更新条数</returns>
        public int Delete<T>(string table, object filter, params T[] dataArray)
        {
            var filterEntities = JsonParser.ConvertEntities(filter);
            if (filterEntities.Count <= 0)
            {
                throw new Exception("未传入筛选条件,未防止错误,该执行已停止.");
            }
            var filterSql = new FilterParser().Parse(table, filterEntities[0]);
            if (string.IsNullOrEmpty(filterSql))
            {
                throw new Exception("未能解析出需要筛选sql语句,未防止错误,该执行已停止.");
            }
            var entities = dataArray == null ? null : JsonParser.ConvertEntities(dataArray);
            var sql = new DeleteParser().Parse(table, entities == null || entities.Count <= 0 ? null : entities[0], null);
            if (string.IsNullOrEmpty(sql))
            {
                throw new Exception("未能解析出需要执行的sql语句.");
            }
            return this.Execute(sql + " WHERE " + filterSql,
                entities == null || entities.Count <= 0 ? null :
                entities.Select((source) =>
                {
                    return new DeleteModel(source, filterEntities[0]);
                }).ToArray());
        }

        /// <summary>
        /// 查询一组记录
        /// </summary>
        /// <param name="table">数据表名称</param>
        /// <param name="filter">过滤条件</param>
        /// <returns>返回数据列表</returns>
        public IEnumerable<T> Select<T>(string table, object filter)
        {
            return this.Select<T>(table, filter, 0);
        }

        /// <summary>
        /// 查询一组记录
        /// </summary>
        /// <param name="table">数据表名称</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="top">查询条数</param>
        /// <returns>返回数据列表</returns>
        public IEnumerable<T> Select<T>(string table, object filter, int top)
        {
            var filterEntities = JsonParser.ConvertEntities(filter);
            if (filterEntities.Count <= 0)
            {
                throw new Exception("未传入筛选条件,未防止错误,该执行已停止.");
            }
            var filterSql = new FilterParser().Parse(table, filterEntities[0]);
            if (string.IsNullOrEmpty(filterSql))
            {
                throw new Exception("未能解析出需要筛选sql语句,未防止错误,该执行已停止.");
            }
            var sql = new SelectParser().Parse(table, top);
            if (string.IsNullOrEmpty(sql))
            {
                throw new Exception("未能解析出需要执行的sql语句.");
            }
            return this.Query<T>(sql + " WHERE " + filterSql);
        }

        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="table">数据表名称</param>
        /// <param name="filter">过滤条件</param>        
        /// <returns>返回数据列表</returns>
        public T SelectSingle<T>(string table, object filter)
        {
            var filterEntities = JsonParser.ConvertEntities(filter);
            if (filterEntities.Count <= 0)
            {
                throw new Exception("未传入筛选条件,未防止错误,该执行已停止.");
            }
            var filterSql = new FilterParser().Parse(table, filterEntities[0]);
            if (string.IsNullOrEmpty(filterSql))
            {
                throw new Exception("未能解析出需要筛选sql语句,未防止错误,该执行已停止.");
            }
            var sql = new SelectParser().Parse(table);
            if (string.IsNullOrEmpty(sql))
            {
                throw new Exception("未能解析出需要执行的sql语句.");
            }           
            var entity = this.QuerySingle<T>(sql + " WHERE " + filterSql);
            return entity;
        }

        #region 私有方法        

        private IDbConnection getConnection(string driver, string conStr)
        {
            switch (driver.ToLower())
            {
                case "mysql":
                    return new MySql.Data.MySqlClient.MySqlConnection(conStr);
                case "sqlite":
                    return new System.Data.SQLite.SQLiteConnection(conStr);
            }
            return null;
        }

        private int Execute(string sql, params object[] entities)
        {
            using (IDbConnection db = getConnection(_driver, this._conStr))
            {
                db.Open();
                return db.Execute(sql, entities);
            }
        }

        private IEnumerable<T> Query<T>(string sql)
        {            
            using (IDbConnection db = getConnection(_driver, this._conStr))
            {
                db.Open();
                return db.Query<T>(sql);                
            }           
        }

        private T QuerySingle<T>(string sql)
        {
            using (IDbConnection db = getConnection(_driver, this._conStr))
            {
                db.Open();
                return db.QueryFirstOrDefault<T>(sql);
            }
        }

        #endregion
    }
}
