using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Commands
{
    public interface ICommand
    {
        string CommandName { get; }

        string PromptMessage { get; }

        void Execute(params string[] args);       
    }
}
