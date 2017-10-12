using Snail.Collector.Modules.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Storage
{
    /// <summary>
    /// 储存模块
    /// </summary>
    public class StorageModule
    {
        #region 内部方法

        public async Task _innerExport(dynamic config, object data, Action<JSCallBackEventArgs> callBack)
        {
            var rest = new JSCallBackEventArgs();
            try
            {
                IStorageProvider provider = getProvider(config.target);
                if (provider == null)
                {
                    throw new Exception("not supported for export type '" + config.target + "'.");
                }
                var dataArray = data as JSArray;
                if (dataArray != null)
                {
                    rest.data = await provider.Export(config, dataArray);
                }
                else
                {
                    rest.data = await provider.ExportSingle(config, data);
                }
                rest.success = (int)rest.data == (dataArray != null ? dataArray.Count : 1);
            }
            catch (Exception ex)
            {
                rest.success = false;
                rest.error = new JSException(ex.Message);
            }
            callBack?.Invoke(rest);
        }

        #endregion

        #region 导出方法

        /// <summary>
        /// 将数据导出到外部存储       
        /// dynamic:{ target:mysql|sqlserver|xml|excel,... }省略各导出模块自定义参数
        /// object: 回调委托 Action<StorageEventArgs>
        /// object: 导出数据集
        /// </summary>
        public Action<dynamic, object, object> export;

        #endregion

        #region 私有成员
        private IStorageProvider getProvider(string name)
        {
            switch (name)
            {
                case "mysql":
                    return new DbProvider();
            }
            return null;
        }
        #endregion



    }
}
