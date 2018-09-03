﻿using Snail.Collector.Common;
using Snail.Collector.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private ICollectRepository _collectRepository;

        public RunCommand(ICollectRepository collectRepository)
        {
            this._collectRepository = collectRepository;
        }

        public void Execute(params string[] args)
        {           
            var parameters = new RunCommandArgs();
            parameters.Parse(args);
            if (!parameters.Success)
            {
                throw new GeneralException(parameters.Error ?? this.PromptMessage);
            }
            var collect = this._collectRepository.SelectSingle(parameters.CollectId);
            if (collect == null)
            {
                throw new GeneralException($"未找到id为:{parameters.CollectId}的任务。");
            }
            CancellationTokenSource cts = new CancellationTokenSource();
            var task = Task.Factory.StartNew(() =>
            {
                for (var idx = 1; idx <= parameters.Repeat; idx++)
                {
                    if (cts.Token.IsCancellationRequested)
                    {
                        break;
                    }
                    try
                    {
                        using (var taskRuntime = TypeContainer.Resolve<CollectTaskRuntime>())
                        {
                            taskRuntime.Start(cts.Token, collect);
                        }
                        Console.WriteLine($"{DateTime.Now} - 已完成一次。");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{DateTime.Now} - 启动异常。");
                        TypeContainer.Resolve<ILogger>().Error($"任务:{parameters.CollectId},启动异常。", ex);                        
                    }
                    finally
                    {
                        if (idx < parameters.Repeat)
                        {
                            Console.WriteLine($"{DateTime.Now} - 将在{parameters.Delay.TotalSeconds} 秒后重新执行。");
                            Thread.Sleep((int)parameters.Delay.TotalMilliseconds);
                        }
                    }
                }
                Console.WriteLine($"{DateTime.Now} - 已结束。");
            });
            Console.WriteLine("正在执行...");
            Console.ReadKey();          
            cts.Cancel();
            while (!task.IsCompleted)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
