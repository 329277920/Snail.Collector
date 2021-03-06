﻿using Snail.Collector.Common;
using System;
using System.IO;
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

        public HttpModule(int timeOut = 100) : base(new HttpClientHandler() { UseCookies = false, UseDefaultCredentials = false, AutomaticDecompression = System.Net.DecompressionMethods.GZip })
        {
            this.headers = new HttpHeaderCollection();
            this.Timeout = DateTime.Now.AddSeconds(timeOut) - DateTime.Now;
        }

        public virtual HttpResult get(string uri)
        {
            try
            {
                var res = this.SendAsync(this.NewHttpReqForGet(uri)).ConfigureAwait(false).GetAwaiter().GetResult();

                return new HttpResult(res);
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, "error", ex);
            }
            return null;
        }

        public virtual HttpResult get(string uri, object data)
        {
            try
            {
                var res = this.SendAsync(this.NewHttpReqForPost(uri, data, HttpContentTypes.Json, HttpMethod.Get)).ConfigureAwait(false).GetAwaiter().GetResult();

                return new HttpResult(res);
            }
            catch (Exception ex)
            {
                LoggerProxy.Error(LogSource, "error", ex);
            }
            return null;
        }

        //public virtual HttpResult postJson(string uri, object data = null)
        //{
        //    var res = this.SendAsync(this.NewHttpReqForPost(uri, data, HttpContentTypes.Json, HttpMethod.Post)).Result;

        //    return new HttpResult(res);
        //}

        //public virtual HttpResult putJson(string uri, object data = null)
        //{
        //    var res = this.SendAsync(this.NewHttpReqForPost(uri, data, HttpContentTypes.Json, HttpMethod.Put)).Result;

        //    return new HttpResult(res);
        //}

        public virtual HttpResult postFile(string uri, string file, string name = "file", string fileName = null)
        {
            if (!File.Exists(file))
            {
                return new HttpResult(null);
            }
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = new FileInfo(file).Name;
            }
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var res = this.SendAsync(this.NewHttpReqForPostFile(uri, fs, name, fileName)).Result;
                return new HttpResult(res);
            }
        }

        public virtual HttpResult post(string uri, object data = null,string contentType = null)
        {
            contentType = contentType ?? HttpContentTypes.FormData;
            var res = this.SendAsync(this.NewHttpReqForPost(uri, data, contentType, HttpMethod.Post)).Result;
            return new HttpResult(res);
        }

        //public virtual HttpResult postForm(string uri, object data = null)
        //{
        //    var res = this.SendAsync(this.NewHttpReqForPost(uri, data, HttpContentTypes.FormData)).Result;

        //    return new HttpResult(res);
        //}

        public Func<string, string, object> postForm;

        public Func<string, string, object> postJson;

        public Func<string, string, object> putJson;

        /// <summary>
        /// 获取默认提交的请求头
        /// </summary>
        public HttpHeaderCollection headers { get; }

        public virtual bool getFiles(params dynamic[] files)
        {
            var downList = (from file in files ?? new dynamic[0]
                            where file != null
                            select new FileDownloader(this,
                            this.NewHttpReqForGet(file.uri),
                            file.savePath)).ToArray();
            return FileDownManager.DownFiles(downList);
        }
        
        #region 私有方法
        private HttpRequestMessage NewHttpReqForGet(string uri)
        {
            return this.NewHttpRequest(uri, HttpMethod.Get);         
        }

        private HttpRequestMessage NewHttpReqForPost(string uri, object data, string contentType, HttpMethod method)
        {
            var req = this.NewHttpRequest(uri, method == null ? HttpMethod.Post : method);
            var strPostData = "";
            switch (contentType)
            {
                case HttpContentTypes.Json:
                    // strPostData = SnailCore.Data.Serializer.JsonSerialize(data);
                    strPostData = data?.ToString();
                    break;
                case HttpContentTypes.FormData:
                    strPostData = data?.ToString();
                    break;                

            }
            if (!string.IsNullOrEmpty(strPostData))
            {
                req.Content = new StringContent(strPostData, System.Text.Encoding.UTF8, contentType);
            }
            return req;
        }

        private HttpRequestMessage NewHttpReqForPostFile(string uri, System.IO.Stream fileStream, string name, string fileName)
        {
            var content = new MultipartFormDataContent(GenerateMultipartBoundary());
            var subContent = new StreamContent(fileStream);           
            subContent.Headers.Add("Content-Type", HttpContentTypes.File);
            subContent.Headers.Add("Content-Disposition",
                string.Format("form-data; name=\"{0}\"; filename=\"{1}\"",
                name,
                fileName));
            content.Add(subContent);
            var req = this.NewHttpRequest(uri, HttpMethod.Post);
            req.Content = content;
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

        /// <summary>
        /// 随机数生成器
        /// </summary>
        private static Lazy<Random> _rand = new Lazy<Random>(() =>
        {
            return new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
        }, true);
        /// <summary>
        /// 生成多表单提交边界值
        /// </summary>
        /// <returns></returns>
        private string GenerateMultipartBoundary()
        {
            return string.Format("--{0}{1}",
               DateTime.Now.Ticks.ToString("x"),
              _rand.Value.Next(10000, 99999));
        }
        #endregion
    }
}
