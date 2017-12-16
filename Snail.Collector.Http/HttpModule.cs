using System.Linq;
using System.Net.Http;

namespace Snail.Collector.Http
{
    /// <summary>
    /// Http客户端
    /// </summary>
    public class HttpModule : HttpClient
    {
        protected string LogSource = "http";

        public HttpModule() : base(new HttpClientHandler() { UseDefaultCredentials = false, AutomaticDecompression = System.Net.DecompressionMethods.GZip })
        {
            this.headers = new HttpHeaderCollection();            
        }

        public virtual HttpResult get(string uri)
        {
            var res = this.SendAsync(this.NewHttpReqForGet(uri)).ConfigureAwait(false).GetAwaiter().GetResult();

            return new HttpResult(res);
        }

        public virtual HttpResult postJson(string uri, object data = null)
        {
            var res = this.SendAsync(this.NewHttpReqForPost(uri, data, HttpContentTypes.Json)).ConfigureAwait(false).GetAwaiter().GetResult();

            return new HttpResult(res);
        }

        /// <summary>
        /// 获取默认提交的请求头
        /// </summary>
        public HttpHeaderCollection headers { get; }

        //public virtual string getStr(string uri)
        //{
        //    var res = this.SendAsync(this.GetReqMsg(uri, HttpMethod.Get)).ConfigureAwait(false).GetAwaiter().GetResult();

        //    return res.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        //}

        //public virtual dynamic getJson(string uri)
        //{
        //    var result = this.getStr(uri);

        //    return Snail.Data.Serializer.JsonDeserialize<dynamic>(result);
        //}

        public virtual bool getFile(params dynamic[] files)
        {
            var downList = (from file in files ?? new dynamic[0]
                            where file != null
                            select new FileDownloader(this,
                            this.NewHttpReqForGet(file.url),
                            file.savePath)).ToArray();
            return FileDownManager.DownFiles(downList);
        }
        
        #region 私有方法
        private HttpRequestMessage NewHttpReqForGet(string uri)
        {
            return this.NewHttpRequest(uri, HttpMethod.Get);         
        }

        private HttpRequestMessage NewHttpReqForPost(string uri, object data, string contentType)
        {
            var req = this.NewHttpRequest(uri, HttpMethod.Post);
            var strPostData = "";
            switch (contentType)
            {
                case HttpContentTypes.Json:
                    strPostData = Snail.Data.Serializer.JsonSerialize(data);
                    break;
            }
            if (data != null)
            {
                req.Content = new StringContent(strPostData, System.Text.Encoding.UTF8, HttpContentTypes.Json);
            }
            return req;
        }

        private HttpRequestMessage NewHttpRequest(string uri, HttpMethod method)
        {
            var reqMsg = new HttpRequestMessage(method, uri);
            if (this.headers.Count > 0)
            {
                foreach (string key in this.headers.Keys)
                {
                    reqMsg.Headers.Add(key, this.headers[key]);
                }
            }
            return reqMsg;
        }
        #endregion
    }
}
