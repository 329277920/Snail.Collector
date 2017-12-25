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

        public string toString(bool autoDispose = true)
        {
            try
            {
                if (this._response == null)
                {
                    return null;
                }
                var result = this._response.Content.ReadAsStringAsync().Result;
                if (autoDispose)
                {
                    this._response.Dispose();
                    this._response = null;
                }                   
                return result;
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, "error", ex);
            }
            return null;
        }

        public bool toFile(string savePath, bool autoDispose = true)
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
            if (autoDispose)
            {
                this._response.Dispose();
                this._response = null;
            }
            return File.Exists(savePath);
        }

        private void dispose()
        {
            try
            {
                this._response?.RequestMessage?.Content?.Dispose();
                this._response?.RequestMessage?.Dispose();
                this._response?.Content?.Dispose();             
                this._response?.Dispose();            
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, "dispose error.", ex);
            }
        }
    }
}
