using Snail.Collector.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Http
{
    /// <summary>
    /// Http请求返回对象
    /// </summary>
    public class HttpResult
    {
        private HttpResponseMessage _response;

        public HttpResult(HttpResponseMessage res)
        {
            this._response = res;
        }

        public string toString()
        {
            return this._response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }     
        
    }
}
