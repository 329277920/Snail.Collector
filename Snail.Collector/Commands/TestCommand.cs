using Snail.Collector.Common;
using Snail.Collector.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Commands
{
    public class TestCommand : ICommand
    {
        public string CommandName => "test";

        public string PromptMessage
        {
            get
            {
                StringBuilder message = new StringBuilder();
                message.Append($"用法: snail test [options]\r\n");
                message.Append($"Options:\r\n");
                message.Append($"  -file: 脚本文件路径\r\n");
                return message.ToString();
            }
        }
        
        private ILogger _logger;

        public TestCommand(ILogger logger)
        {           
            this._logger = logger;
        }

        public void Execute(params string[] args)
        {
            var parameters = new TestCommandArgs();
            parameters.Parse(args);
            if (!parameters.Success)
            {
                throw new GeneralException(parameters.Error ?? this.PromptMessage);
            }
            try
            {
                using (var initInvoker = new CollectTaskInvoker(new CollectTaskState()))
                {
                    initInvoker.Invoke(new Model.CollectInfo()
                    {
                        Id = 0,
                        ScriptFilePath = parameters.ScriptFilePath,
                        Name = "test"
                    }, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} - 执行异常。\r\n{ex}");
            }             
        }       
    }
}
