using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Http
{
    /// <summary>
    /// 文件下载器，用于封装进线程池中
    /// </summary>
    internal class FileDownloader
    {
        private HttpModule _http;

        private HttpRequestMessage _reqMsg;

        private string _savePath;

        /// <summary>
        /// 使用指定的Http客户端初始化文件下载器
        /// </summary>
        /// <param name="http">HttpClient</param>
        /// <param name="reqMsg">请求消息体</param>
        /// <param name="savePath">文件路径</param>
        public FileDownloader(HttpModule http, HttpRequestMessage reqMsg, string savePath)
        {
            this._http = http;
            this._reqMsg = reqMsg;
            this._savePath = savePath;
            reqMsg.Headers.Range = new RangeHeaderValue();
        }

        /// <summary>
        /// 开始下载
        /// </summary>
        public bool Down()
        {
            try
            {
                if (File.Exists(this._savePath))
                {
                    return true;
                }               
                using (var localStream = GetLocalStream())
                {
                    this._reqMsg.Headers.Range = new RangeHeaderValue(localStream.Length, null);
                    var res = this._http.SendAsync(this._reqMsg).ConfigureAwait(false).GetAwaiter().GetResult();
                    using (var remoteStream = res.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult())
                    {
                        remoteStream.CopyTo(localStream);
                    }                       
                }                  
                this.Compelte();
                return File.Exists(this._savePath);
            }
            catch (Exception ex)
            {
              
                return false;
            }            
        }

        /// <summary>
        /// 获取本地文件流
        /// </summary>        
        /// <returns></returns>
        private Stream GetLocalStream()
        {
            var tempFile = string.Format("{0}_bak", this._savePath);

            return new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.None);
        }

        /// <summary>
        /// 完成下载
        /// </summary>
        private void Compelte()
        {
            if (File.Exists(this._savePath))
            {
                return;
            }
            File.Move(string.Format("{0}_bak", this._savePath), this._savePath);
        }
    }
}
