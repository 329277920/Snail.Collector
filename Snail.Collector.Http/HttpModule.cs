using System.Linq;
using System.Net.Http;

namespace Snail.Collector.Http
{
    /// <summary>
    /// Http客户端
    /// </summary>
    public class HttpModule : HttpClient
    {
        public HttpModule() : base(new HttpClientHandler() { UseDefaultCredentials = false })
        {
            this.headers = new HttpHeaderCollection();
        }

        /// <summary>
        /// 获取默认提交的请求头
        /// </summary>
        public HttpHeaderCollection headers { get; }

        public virtual string getStr(string uri)
        {
            var res = this.SendAsync(this.GetReqMsg(uri, HttpMethod.Get)).ConfigureAwait(false).GetAwaiter().GetResult();

            return res.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual dynamic getJson(string uri)
        {
            var result = this.getStr(uri);

            return Snail.Data.Serializer.JsonDeserialize<dynamic>(result);
        }

        public virtual bool getFile(params dynamic[] files)
        {
            var downList = (from file in files ?? new dynamic[0]
                            select new FileDownloader(this,
                            this.GetReqMsg(file.uri, HttpMethod.Get),
                            file.savePath)).ToArray();
            return FileDownManager.DownFiles(downList);
        }
        
        #region 私有方法
        private HttpRequestMessage GetReqMsg(string uri, HttpMethod method)
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
