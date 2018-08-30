using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules
{
    public class JSArray : List<dynamic>
    {
        public JSArray push(params dynamic[] items)
        {
            if (items == null) {
                return this;
            }
            foreach (var item in items)
            {
                if (item is JSArray)
                {
                    this.AddRange(item as JSArray);
                    continue;
                }
                this.Add(item);
            }
            return this;
        }

        public int indexOf(dynamic item)
        {
            return this.IndexOf(item);
        }

        public int length { get { return this.Count; } }

        public JSArray each(dynamic function) {
            foreach (var item in this)
            {
                function(item);
            }
            return this;
        }

        public JSArray() : base() { }
    }
}
