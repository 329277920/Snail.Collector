using System.Net;

namespace Snail.Collector.Http
{
    public class HttpHeaderCollection : WebHeaderCollection
    {
        public void add(string name, string value)
        {
            base.Add(name, value);
        }
        public void remove(string name)
        {
            base.Remove(name);
        }
    }
}
