using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Common
{
    public interface ILogger
    {
        void Error(string message, Exception ex = null);

        void Info(string message);
    }
}
