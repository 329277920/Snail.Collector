using Microsoft.Owin.Hosting;
using Snail.Collector.Core;
using System;

namespace Snail.Collector.Host
{
    class Program
    {
        static void Main(string[] args)
        {

            //var str = "[{\"id\":1},{ user:\"cnf\", age :10, \"id\" : { \"$p\": \"id\" },\"status\" : { \"$in\": [1,2,3] } }]";

            //var entity = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(str);



            //var sql = JsonParser.ToSqlQuery(entity);

            //Console.WriteLine(entity.user);

            TaskFactory.OnTaskRunning += (sender, e) => {
                System.Diagnostics.Debug.WriteLine(string.Format("Task:{0},开始运行", e.Task.TaskName));
            };

            TaskFactory.OnTaskComplete += (sender, e) => {
                System.Diagnostics.Debug.WriteLine(string.Format("Task:{0},结束运行", e.Task.TaskName));
            };

            TaskFactory.InitTasks("tasks");

            var tasks = TaskFactory.Tasks;

            foreach (var task in tasks)
            {
                // if (task.TaskName == "测试数据持久层")
                if (task.TaskName == "测试Http")
                {
                    TaskFactory.Run(task.TaskId);
                }
            }

            System.Diagnostics.Debug.WriteLine("主程序执行结束");

            Console.ReadKey();

            using (WebApp.Start<Startup>(GetStartOptions()))
            {
                Console.WriteLine("OK");
                // Console.WriteLine("Server run at " + url + " , press Enter to exit.");
                Console.ReadLine();
            }

            Console.ReadKey();

            // Snail.Collector.Core.TaskFactory.Run("");
        }

        private static StartOptions GetStartOptions()
        {
            var opt = new StartOptions();
            opt.Urls.Add("http://localhost:9091/");
            opt.Urls.Add("http://localhost:9092/");
            return opt;
        }
    }
}
