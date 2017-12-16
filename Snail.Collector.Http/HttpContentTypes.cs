using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Http
{
    /// <summary>
    /// Http内容格式
    /// </summary>
    public class HttpContentTypes
    {
        public const string FormData = "application/x-www-form-urlencoded";

        public const string WebApi = "application/webapi";

        public const string Json = "application/json";

        public const string Xml = "application/xml";

        public const string String = "text/plain";

        public const string MultipartFormData = "multipart/form-data";
    }
}
