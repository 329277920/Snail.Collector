using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Net.Http
{
    public class HttpModule : HttpClient
    {
        /// <summary>
        /// 获取该对象的请求头
        /// </summary>
        public HttpRequestHeaders headers
        {
            get { return this.DefaultRequestHeaders; }
        }

        /// <summary>
        /// 获取网页源代码
        /// </summary>
        /// <param name="url">网页地址</param>
        /// <param name="callBack">获取完成后回调方法</param>
        /// <returns>返回一个等待Task</returns>
        public async Task innerGetString(string url, Action<string> callBack)
        {
            var content = await GetStringAsync(url);

            callBack?.Invoke(content);


        }

        /// <summary>
        /// 获取网页源代码
        /// </summary>
        /// <param name="url">网页地址</param>
        /// <param name="callBack">获取完成后回调方法</param>
        /// <returns>返回一个等待Task</returns>
        public Func<string, Action<string>> getString;
    }
}
