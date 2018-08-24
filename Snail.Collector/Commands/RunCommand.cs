using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Commands
{
    public class RunCommand : ICommand
    {
        public string CommandName => "run";

        public string PromptMessage
        {
            get
            {
                StringBuilder message = new StringBuilder();
                message.Append($"用法: snail run [options]\r\n");
                message.Append($"Options:\r\n");
                message.Append($"  -id: 任务id\r\n");
                return message.ToString();
            }
        }

        public void Execute(params string[] args)
        {
            var parameters = new RunCommandArgs();
            parameters.Parse(args);
            if (!parameters.Success)
            {
                throw new ParameterFailedException(parameters.Error ?? this.PromptMessage);
            }
        }
    }
}
