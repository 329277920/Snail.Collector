using Snail.Collector.Modules.Html;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Collections.Generic;
using Microsoft.ClearScript.V8;

namespace Snail.Collector.Modules.Http
{
    /// <summary>
    /// Http模块
    /// </summary>
    public class HttpModule : IInitModule
    {        
        private HttpClient _client;
        private V8ScriptEngine _scriptEngine;
        private IFileDownManager _fileDowner;

        public HttpModule(IFileDownManager fileDowner)
        {
            this._fileDowner = fileDowner;
            this._client = new HttpClient(new HttpClientHandler()
            {
                UseCookies = false,
                UseDefaultCredentials = false,
                AutomaticDecompression = System.Net.DecompressionMethods.GZip
            });
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
            return new HttpResult(this._scriptEngine, _client.SendAsync(request).Result);
        }

        public virtual int getFiles(params dynamic[] files)
        {
            if (files == null || files.Length <= 0)
            {
                return 0;
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
            if (this._fileDowner.DownFiles(downList))
            {
                return downList.Length;
            }
            return -1;
        }

        public void Init(V8ScriptEngine scriptEngine)
        {
            this._scriptEngine = scriptEngine;
            this._scriptEngine.AddHostType(typeof(HttpRequestHeadersExtends));
        }
    }
}
