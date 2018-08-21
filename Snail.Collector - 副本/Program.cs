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
            if (args.Length <= 0)
            {
                InputPromptMessage();
                return;
            }
            foreach (var arg in args)
            {
                switch (arg)
                {
                    case "-id":
                        break;
                    case "-f":
                        break;
                }
            }

            var workDirectory = Directory.GetCurrentDirectory();                    
        }

        static void InputPromptMessage()
        {
            Console.WriteLine($"用法: snail [options]\r\n");
            Console.WriteLine($"Options:");
            Console.WriteLine($"  -h: 显示帮助");
            Console.WriteLine($"  -v: 显示版本");
        }

        static void InputHelpMessage()
        {

        }
    }
}
