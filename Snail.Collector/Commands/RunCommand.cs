using Snail.Collector.Common;
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
        private ILogger _logger;

        public RunCommand(
            ICollectRepository collectRepository,
            ILogger logger)
        {
            this._collectRepository = collectRepository;
            this._logger = logger;
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
            Console.WriteLine($"准备执行任务:{collect.Name}");
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
                            int consolePosition = Console.CursorTop;
                            taskRuntime.OnCollectTaskInvokeComplete += (sender, e) =>
                            {
                                if (!e.Success)
                                {
                                    var message = $"执行失败({e.Task.CollectId}-{e.Task.Id}),地址:{e.Task.Uri}";
                                    if (e.Error != null && e.Error.Message != null && e.Error.Message.StartsWith("Error: UserError:"))
                                    {
                                        message += ("\r\n" + e.Error.Message);
                                        this._logger.Error(message);
                                        return;
                                    }
                                    this._logger.Error(message, e.Error);
                                }
                                else
                                {
                                    var refTaskRuntime = sender as CollectTaskRuntime;
                                    lock (taskRuntime)
                                    {
                                        Console.SetCursorPosition(0, consolePosition);                                        
                                        Console.WriteLine($"正在执行任务数:{refTaskRuntime.State.RunningTaskCount.ToString().PadLeft(10, ' ')}");
                                        Console.WriteLine($"正常完成任务数:{refTaskRuntime.State.CompleteTaskCount.ToString().PadLeft(10, ' ')}");
                                        Console.WriteLine($"执行异常任务数:{refTaskRuntime.State.ErrorTaskCount.ToString().PadLeft(10, ' ')}");
                                        Console.WriteLine($"新增任务数:{refTaskRuntime.State.NewTaskCount.ToString().PadLeft(10, ' ')}");
                                        Console.WriteLine($"新增文件数:{refTaskRuntime.State.NewFileCount.ToString().PadLeft(10, ' ')}");
                                        Console.WriteLine($"新增内容数:{refTaskRuntime.State.NewContentCount.ToString().PadLeft(10, ' ')}");
                                    }                                   
                                }
                            };
                            taskRuntime.Start(cts.Token, collect);
                        }
                        Console.WriteLine($"{DateTime.Now} - 已完成一次。");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{DateTime.Now} - 启动异常。");
                        this._logger.Error($"任务:{parameters.CollectId},启动异常。", ex);                        
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
            Console.ReadKey();          
            cts.Cancel();
            while (!task.IsCompleted)
            {
                Thread.Sleep(1000);
            }
        }

        private void TaskRuntime_OnCollectTaskInvokeComplete(object sender, Core.CollectTaskInvokeCompleteArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
