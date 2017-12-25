using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Modules.Core
{
    public class JSArray : List<object>
    {
        public void push(object item)
        {
            this.Add(item);
        }

        public JSArray() : base() { }
    }
}
