using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Commands
{
    /// <summary>
    /// 封装一个新增任务的命令
    /// </summary>
    public class AddCommand : ICommand
    {
        public string CommandName => "add";

        public string PromptMessage
        {
            get
            {
                StringBuilder message = new StringBuilder();
                message.Append($"用法: snail add [options]\r\n");
                message.Append($"Options:\r\n");
                message.Append($"  -id: 任务id\r\n");
                message.Append($"  -name: 任务名称\r\n");
                message.Append($"  -file: 脚本文件路径\r\n");
                return message.ToString();
            }
        }

        public void Execute(params string[] args)
        {
            var parameters = new AddCommandArgs();
            parameters.Parse(args);
            if (!parameters.Success)
            {
                throw new ParameterFailedException(parameters.Error ?? this.PromptMessage);
            }
        }
    }
}
