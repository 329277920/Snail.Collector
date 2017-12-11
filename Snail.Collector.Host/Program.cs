using Snail.Collector.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Snail.Collector.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskFactory.OnTaskRunning += (sender, e) => {
                System.Diagnostics.Debug.WriteLine(string.Format("Task:{0},开始运行", e.Task.TaskName));
            };

            TaskFactory.OnTaskComplete += (sender, e) => {
                System.Diagnostics.Debug.WriteLine(string.Format("Task:{0},结束运行，新增子任务数:{1}", e.Task.TaskName, e.Task.Context.Stat.NewTaskCount));
            };

            var task = TaskFactory.InitTask("tasks/demo/task.js");

            TaskFactory.Run(task.TaskId);

            // TaskFactory.InitTasks("tasks");

            // var tasks = TaskFactory.Tasks;



            Console.ReadKey();
        }
    }
}
