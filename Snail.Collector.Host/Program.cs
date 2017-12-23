using Snail.Collector.Common;
using Snail.Collector.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Snail.Collector.Host
{
    class Program
    {
        private const string LogSource = "main";

        static void Main(string[] args)
        {
            System.AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var error = new StringBuilder("an unhandled exception occurred in the application.");
                if (e?.ExceptionObject != null)
                {
                    error.AppendFormat("\r\n{0}", e.ExceptionObject.ToString());
                }
                LoggerProxy.Error(LogSource, error.ToString());                
            };
             
            TaskFactory.OnTaskRunning += (sender, e) => {
                Console.WriteLine(string.Format("Task:{0},开始运行", e.Task.TaskName));               
            };

            TaskFactory.OnTaskComplete += (sender, e) => {
                Console.WriteLine(string.Format("Task:{0},结束运行，新增子任务数:{1}", e.Task.TaskName, e.Task.Stat.NewTaskCount));                
            };

            var task = TaskFactory.InitTask("tasks/jd/task.js");

            TaskFactory.Run(task.TaskId);

            // TaskFactory.InitTasks("tasks");

            // var tasks = TaskFactory.Tasks;



            Console.ReadKey();
        }
    }
}
