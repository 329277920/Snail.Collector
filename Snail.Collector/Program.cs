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

        }

        static void InputPromptMessage()
        {
            Console.WriteLine($"用法: snail [command] [options]\r\n");
            Console.WriteLine($"Command:");            
            Console.WriteLine($"  run: 执行某个采集任务");
            Console.WriteLine($"  add: 添加采集任务");
        }

        static void InputHelpMessage()
        {

        }
    }
}
