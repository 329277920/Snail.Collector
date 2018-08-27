using Snail.Collector.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

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
            args = new string[] { "add", "-file", "Script/demo-index.js", "-id", "1", "-name", "demo" };
          
            if (args == null || args.Length == 0)
            {
                InputPromptMessage();
                return;
            }
            var command = TypeContainer.Resolve<ICommand>($"task_{args[0]}");
            if (command == null)
            {
                InputPromptMessage();
                return;
            }
            try
            {
                command.Execute(args.Skip(1).ToArray());
            }
            catch (GeneralException glEx)
            {
                Console.WriteLine(glEx.Message);
            }       
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();
        }

        static void InputPromptMessage()
        {
            Console.WriteLine($"用法: snail [command] [options]\r\n");
            Console.WriteLine($"Command:");            
            Console.WriteLine($"  run: 执行某个采集任务");
            Console.WriteLine($"  add: 添加采集任务");
        }              
    }
}
