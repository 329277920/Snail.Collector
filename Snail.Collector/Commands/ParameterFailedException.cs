using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Commands
{
    public class ParameterFailedException : Exception
    {
        public ParameterFailedException(string message) : base(message) { }
    }
}
