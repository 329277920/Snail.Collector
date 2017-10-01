using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Net.Http
{
    public class HttpModule : HttpClient
    {
        #region 定义内部方法

        public async Task _innerGetString(string uri, Action<string> callBack)
        {
            var content = await GetStringAsync(uri);

            callBack?.Invoke(content);
        }

        public async Task _innerGetString(string uri, Func<string, string> callBack)
        {
            var content = await GetStringAsync(uri);

            var result = callBack?.Invoke(content);

            if (result?.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine(result);
            }
        }
        #endregion

        #region 定义导出方法（用于方便js调用）

        /// <summary>
        /// 在module.js代理脚本中实现，object为Action<String>
        /// </summary>
        public Action<string, object> getString;

        /// <summary>
        /// 在module.js代理脚本中实现，object为Action<String>
        /// </summary>
        public Action<string, object> getString2;

        #endregion         
    }
}
