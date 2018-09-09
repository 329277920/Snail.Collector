using Microsoft.Extensions.Configuration;
using Snail.Collector.Commands;
using Snail.Collector.Common;
using Snail.Collector.Common.Sync;
using Snail.Collector.Core;
using Snail.Collector.Modules;
using Snail.Collector.Modules.Html;
using Snail.Collector.Modules.Http;
using Snail.Collector.Repositories;
using System;
using System.Linq;
using System.Threading;
using Unity;

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
            ApplicationStart();

            //args = new string[] { "add", "-file", "Script/xxx/100.js", "-id", "100", "-name", "xxx" };
            //args = new string[] { "run", "-id", "100" };

            // args = new string[] { "add", "-file", "Script/demo-index.js", "-id", "10", "-name", "广点通-后台数据" };
            // args = new string[] { "run", "-id", "10", "-repeat", "10", "-delay", "01:00:00" };

            args = new string[] { "test","-file", @"D:\Projects\Snail.Collector\Snail.Collector\Script\manhua\300-test.js" };

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static void ApplicationStart()
        {
            // 加载配置
            var configurationBuidler = new ConfigurationBuilder().AddJsonFile("appconfig.json");
#if DEBUG
            configurationBuidler.AddJsonFile("appconfig.debug.json", true);
#endif                                   
            var configRoot = configurationBuidler.Build();

            SqliteProxy.Init(configRoot["db:path"]);

            TypeContainer.Container.RegisterInstance<IConfiguration>(configRoot);

            TypeContainer.Container.RegisterSingleton<ICollectRepository, CollectRepository>();
            TypeContainer.Container.RegisterSingleton<ICollectTaskRepository, CollectTaskRepository>();
            TypeContainer.Container.RegisterSingleton<ICollectContentRepository, CollectContentRepository>();
            TypeContainer.Container.RegisterSingleton<ILogger, Log4NetLogger>();
            TypeContainer.Container.RegisterSingleton<ICommand, AddCommand>("task_add");
            TypeContainer.Container.RegisterSingleton<ICommand, RunCommand>("task_run");
            TypeContainer.Container.RegisterSingleton<ICommand, TestCommand>("task_test");

            // 注册模块
            TypeContainer.Container.RegisterSingleton<IFileDownManager, FileDownManager>();
            TypeContainer.Container.RegisterSingleton<HttpModule>();
            TypeContainer.Container.RegisterSingleton<HtmlModule>();
            TypeContainer.Container.RegisterSingleton<DebugModule>();
            TypeContainer.Container.RegisterSingleton<LoggerModule>();

            TypeContainer.Container.RegisterType<CollectTaskAccessProxy>();
            TypeContainer.Container.RegisterType<CollectTaskRuntime>();            
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
