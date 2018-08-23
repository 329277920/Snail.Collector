using Snail.Collector.Repositories;
using Snail.Collector.StartArgs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector
{
    class Program
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                InputPromptMessage();
                return;
            }
            var comand = args[0];
            switch (comand)
            {
                case "run":
                    var runArgs = new RunArgs();
                    runArgs.Parse(args.Skip(1).ToArray());
                    if (!runArgs.Success)
                    {
                        InputRunPromptMessage(runArgs.Error);
                    }                     
                    break; ;
                case "add":
                    var addArgs = new AddArgs();
                    addArgs.Parse(args.Skip(1).ToArray());
                    if (!addArgs.Success)
                    {
                        InputAddPromptMessage(addArgs.Error);
                        return;
                    }
                    break;
                default:
                    InputPromptMessage();
                    return;
            }
        }

        static void InputPromptMessage()
        {
            Console.WriteLine($"用法: snail [command] [options]\r\n");
            Console.WriteLine($"Command:");            
            Console.WriteLine($"  run: 执行某个采集任务");
            Console.WriteLine($"  add: 添加采集任务");
        }

        static void InputAddPromptMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine($"error: {message}");
            }
            Console.WriteLine($"用法: snail add [options]\r\n");
            Console.WriteLine($"Options:");
            Console.WriteLine($"  -id: 任务id");
            Console.WriteLine($"  -name: 任务名称");
            Console.WriteLine($"  -file: 脚本文件路径");
        }

        static void InputRunPromptMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine($"error: {message}");
            }
            Console.WriteLine($"用法: snail run [options]\r\n");
            Console.WriteLine($"Options:");
            Console.WriteLine($"  -id: 任务id");          
        }

        static void ExecAdd(AddArgs args)
        {
            TypeContainer.Resolve<ICollectRepository>();
        }
    }
}
