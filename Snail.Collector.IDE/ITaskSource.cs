using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.IDE
{
    public interface ITaskSource
    {
        T Next<T>();
    }
}
