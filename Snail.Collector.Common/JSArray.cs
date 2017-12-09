using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.JSAdapter
{
    public class JSArray : List<dynamic>
    {
        public void push(dynamic item)
        {
            this.Add(item);
        }

        public int indexOf(dynamic item)
        {
            return this.IndexOf(item);
        }

        public int length { get { return this.Count; } }

        public void each(dynamic function) {
            foreach (var item in this)
            {
                function(item);
            }
        }

        public JSArray() : base() { }
    }
}
