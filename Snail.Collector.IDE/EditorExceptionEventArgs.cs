using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.IDE
{
    public class EditorExceptionEventArgs : EventArgs
    {
        public Exception Ex { get; set; }

        public string Message { get; set; }
    }
}
