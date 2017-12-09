﻿using Newtonsoft.Json;
using Snail.Collector.JSAdapter;
using Snail.Collector.Storage.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snail.Collector.Storage
{
    /// <summary>
    /// 内容存储模块
    /// </summary>
    public class StorageDataModule
    {
        private dynamic _cfg;

        private void config(dynamic config)
        {
            _cfg = config;
        }

        /// <summary>
        /// 新增一条或一组记录
        /// </summary>        
        /// <param name="table">数据表名称</param>
        /// <param name="data">需要插入的记录</param>
        /// <returns>返回新增条数</returns>
        public int insert(string table, params object[] data)
        {
            try
            {
                IStorageProvider provider = this.getProvider(_cfg);
                return provider.Insert(table, data);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                // todo: 写日志                 
            }
            return 0;
        }

        /// <summary>
        /// 更新一条或一组记录
        /// </summary>       
        /// <param name="table">数据表名称</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="data">需要更新的记录</param>
        /// <returns>返回更新条数</returns>
        public int update(string table, object filter, params object[] data)
        {
            try
            {
                IStorageProvider provider = this.getProvider(_cfg);
                return provider.Update(table, filter, data);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                // todo: 写日志                 
            }
            return 0;
        }

        /// <summary>
        /// 更新一条或一组记录
        /// </summary>     
        /// <param name="table"></param>
        /// <param name="filter"></param>
        /// <param name="data">需要删除的记录</param>
        /// <returns>返回删除行数</returns>
        public int delete(string table, object filter, params object[] data)
        {
            try
            {
                IStorageProvider provider = this.getProvider(_cfg);
                return provider.Delete(table, filter, data);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                // todo: 写日志                 
            }
            return 0;
        }

        /// <summary>
        /// 查询一组记录
        /// </summary>
        public Func<object, string, object, JSArray> select;

        /// <summary>
        /// 查询单条记录
        /// </summary>
        public Func<object, string, object, object> single;

        #region 私有成员

        private IStorageProvider getProvider(dynamic config)
        {
            switch (config.driver)
            {
                case "mysql":
                case "sqlite":
                    return new DbProvider(config);
            }
            throw new Exception("not supported for export type '" + config.target + "'.");
        }

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="config">存储配置</param>
        /// <param name="table">数据表名称</param>
        /// <param name="filter">过滤条件</param>
        /// <returns>返回js数组</returns>
        public JSArray InnerSelect(dynamic config, string table, object filter)
        {
            JSArray result = new JSArray();
            try
            {
                IStorageProvider provider = this.getProvider(config);
                var data = provider.Select<dynamic>(table, filter);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        result.Add(JsonConvert.SerializeObject(item));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                // todo: 写日志                 
            }
            return result;
        }

        /// <summary>
        /// 查询记录单条
        /// </summary>
        /// <param name="config">存储配置</param>
        /// <param name="table">数据表名称</param>
        /// <param name="filter">过滤条件</param>
        /// <returns>返回js数组</returns>
        public dynamic InnerSelectSingle(dynamic config, string table, object filter)
        {
            JSArray array = this.InnerSelect(config, table, filter);
            if (array == null || array.Count <= 0)
            {
                return null;
            }
            return array[0];
        }

        #endregion
    }
}
