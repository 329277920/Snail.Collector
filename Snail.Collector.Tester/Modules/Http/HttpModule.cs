using Snail.Collector.Modules.Html;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Collections.Generic;

namespace Snail.Collector.Modules.Http
{
    /// <summary>
    /// Http模块
    /// </summary>
    public class HttpModule
    {
        private static object _lock = new object();
        private static HttpClient _client;

        public HttpModule()
        {
            if (_client == null)
            {
                lock (_lock)
                {
                    if (_client == null)
                    {
                        _client = new HttpClient(new HttpClientHandler()
                        {
                            UseCookies = false,
                            UseDefaultCredentials = false,
                            AutomaticDecompression = System.Net.DecompressionMethods.GZip
                        });
                    }
                }
            }
        }

        public HttpRequestHeaders headers
        {
            get { return _client.DefaultRequestHeaders; }
        }

        public HttpResult get(string uri)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(uri),
                Method = HttpMethod.Get
            };
            return new HttpResult(_client.SendAsync(request).Result);
        }

        public virtual bool getFiles(params dynamic[] files)
        {
            if (files == null || files.Length <= 0)
            {
                return true;
            }
            List<dynamic> destArray = new List<dynamic>();
            foreach (var item in files)
            {
                if (item is JSArray)
                {
                    destArray.AddRange(item as JSArray);
                    continue;
                }
                destArray.Add(item);
            }
            var downList = (from file in destArray
                            where file != null
                            select new FileDownloader(_client,
                            new HttpRequestMessage()
                            {
                                RequestUri = new Uri(file.uri),
                                Method = HttpMethod.Get
                            },
                            file.savePath)).ToArray();
            return FileDownManager.DownFiles(downList);
        }
    }
}
