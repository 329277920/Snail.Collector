using Snail.Collector.Modules.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Http
{
    public class HttpResult
    {
        private HttpResponseMessage _response;
        public HttpResult(HttpResponseMessage response)
        {
            this._response = response;
        }

        public HttpResponseHeaders headers { get { return this._response.Headers; } }

        public string toString()
        {
            using (this._response)
            {
                return this._response.Content.ReadAsStringAsync().Result;
            }
        }

        public bool toFile(string savePath)
        {
            using (this._response)
            {
                using (var localStream = new FileStream(savePath, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (var remoteStream = this._response.Content.ReadAsStreamAsync().Result)
                    {
                        remoteStream.CopyTo(localStream);
                    }
                }
            }
            return File.Exists(savePath);
        }

        public Element toHtml()
        {
            return new Element(this.toString());
        }

        // todo : 未实现
        //public dynamic toJson()
        //{
        //    var strJson = this.toString();
        //    if (string.IsNullOrEmpty(strJson))
        //    {
        //        return null;
        //    }
        //    return TaskInvokerContext.Current.Engine.Script.JSON.parse(strJson);
        //}       
    }
}
