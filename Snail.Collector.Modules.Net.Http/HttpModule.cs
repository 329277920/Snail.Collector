using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using System.IO;
using System.Net.Http.Headers;

namespace Snail.Collector.Modules.Net.Http
{
    public class HttpModule : HttpClient
    {
        public HttpModule() : base(new HttpClientHandler() { UseCookies = false })
        {

        }

        #region 定义内部方法

        public async Task _innerGetString(string uri, Action<string> callBack)
        {
            var content = await GetStringAsync(uri);

            callBack?.Invoke(content);
        }

        public async Task _innerGetJson(string uri, Action<object> callBack)
        {
            try
            {
                var content = await GetStringAsync(uri);
                object json = null;
                if (content?.Length > 0)
                {
                    json = Snail.Seriazliation.Serializer.JsonDeserialize<dynamic>(content);
                }
                callBack?.Invoke(json);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task _innerDownfile(string uri, string savePath, Action<string> callBack)
        {
            try
            {
                var remoteStream = await GetStreamAsync(uri).ConfigureAwait(false);
                using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    await remoteStream.CopyToAsync(fs).ConfigureAwait(false);
                }
                callBack?.Invoke(savePath);
            }
            catch (Exception ex)
            {
                callBack?.Invoke(ex.Message);
            }
        }

        public async Task _innerGetString(string uri, Func<string, string> callBack)
        {
            var content = await GetStringAsync(uri);

            var result = callBack?.Invoke(content);

            if (result?.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine(result);
            }
        }
        #endregion

        #region 定义导出方法（用于方便js调用）

        /// <summary>
        /// 在module.js代理脚本中实现，object为Action<String>
        /// </summary>
        public Action<string, object> getString;

        /// <summary>
        /// 在module.js代理脚本中实现，object为Action<String>
        /// </summary>
        public Action<string, object> getString2;

        public Action<string, object> getJson;

        public Func<string, string, object, Task> downFile;

        #endregion

        #region 导出属性

        public HttpRequestHeaders headers
        {
            get { return DefaultRequestHeaders; }
        }

        #endregion
    }
}
