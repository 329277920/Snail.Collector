using Snail.Collector.Common;
using Snail.Collector.Html;
using System;
using System.Collections.Generic;
using System.IO;
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
        private string LogSource = "httpResult";

        private HttpResponseMessage _response;

        public HttpResult(HttpResponseMessage res)
        {
            this._response = res;
        }

        public string toString()
        {
            try
            {
                if (this._response == null)
                {
                    return null;
                }
                var result =  this._response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                // LoggerProxy.Info(LogSource, result);
                return result;
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, "error", ex);
            }
            return null;
        }

        public bool toFile(string savePath)
        {
            if (this._response == null)
            {
                return false;
            }
            using (var localStream = new FileStream(savePath, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                using (var remoteStream = this._response.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult())
                {
                    remoteStream.CopyTo(localStream);
                }
            }
            return File.Exists(savePath);
        }
    }
}
