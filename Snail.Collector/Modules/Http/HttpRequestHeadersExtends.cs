using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Http
{
    public static class HttpRequestHeadersExtends
    {
        public static void add(this HttpRequestHeaders headers, string name, string value)
        {
            if (!headers.Contains(name))
            {
                headers.Add(name, value);
            }
            else
            {
                headers.Remove(name);
                headers.Add(name, value);
            }
        }
    }
}
