using Snail.Collector.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 采集任务工厂类
    /// </summary>
    public static class TaskFactory
    {
        private static Dictionary<string, Task> BufferTasks = new Dictionary<string, Task>();

        private static object LockObj = new object();

        private const string LogSource = "taskfactory";

        /// <summary>
        /// 当任务运行时触发
        /// </summary>
        public static event EventHandler<TaskEventArgs> OnTaskRunning;

        /// <summary>
        /// 在任务结束运行时触发
        /// </summary>
        public static event EventHandler<TaskEventArgs> OnTaskComplete;

        /// <summary>
        /// 在任务发生异常，未能启动时发生
        /// </summary>
        public static event EventHandler<TaskErrorEventArgs> OnTaskError;

        /// <summary>
        /// 初始化一个任务
        /// </summary>
        /// <param name="cfgFile">任务配置文件路径，如果已经初始化，则返回任务ID</param>        
        /// <returns>返回任务对象</returns>
        public static Task InitTask(string cfgFile)
        {
            var fullPath = Snail.IO.PathUnity.GetFullPath(cfgFile);
            if (string.IsNullOrEmpty(fullPath))
            {
                throw new Exception("could not find the file with path:" + cfgFile);
            }
            Task task = null;
            lock (LockObj)
            {
                if (!BufferTasks.ContainsKey(fullPath))
                {
                    task = new Task(fullPath);
                    foreach (var kv in BufferTasks)
                    {
                        if (kv.Value.TaskId == task.TaskId)
                        {
                            throw new Exception(string.Format("任务id:{0},重复出现,请检查配置,并重新启动任务.", task.TaskId));
                        }
                    }
                    task.OnStart += (sender, e) =>
                    {
                        try
                        {
                            OnTaskRunning?.Invoke(sender, e);
                        }
                        catch (Exception ex)
                        {
                            LoggerProxy.Error(LogSource, "invoke OnTaskRunning failed.", ex);
                        }
                    };
                    task.OnStop += (sender, e) =>
                    {
                        try
                        {
                            OnTaskComplete?.Invoke(sender, e);
                        }
                        catch (Exception ex)
                        {
                            LoggerProxy.Error(LogSource, "invoke OnTaskComplete failed.", ex);
                        }
                    };
                    task.OnError += (sender, e) =>
                    {
                        try
                        {
                            OnTaskError?.Invoke(sender, e);                            
                        }
                        catch (Exception ex)
                        {
                            LoggerProxy.Error(LogSource, "invoke OnTaskError failed.", ex);
                        }
                    };
                    BufferTasks.Add(fullPath, task);
                }
            }
            return BufferTasks[fullPath];
        }

        /// <summary>
        /// 初始化某个目录下定义的所有任务
        /// </summary>
        /// <param name="cfgFolder">配置文件目录</param>
        public static void InitTasks(string cfgFolder)
        {
            cfgFolder = Snail.IO.PathUnity.GetFullPath(cfgFolder);
            if (string.IsNullOrEmpty(cfgFolder))
            {
                throw new Exception("cannot found the folder with path : '" + cfgFolder + "'");
            }
            FileUnity.Each(new DirectoryInfo(cfgFolder), (file) =>
            {
                if (file.Name.Equals("task.js"))
                {
                    InitTask(file.FullName);
                }
            });
        }

        /// <summary>
        /// 执行某个任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        public static void Run(int taskId)
        {
            var task = SafeGetTask(taskId);
            if (task == null)
            {
                throw new Exception("cannot found the task with id '" + taskId + "'.");
            }
            task.Run();
        }

        /// <summary>
        /// 获取所有任务集合
        /// </summary>
        /// <returns></returns>
        public static Task[] Tasks
        {
            get
            {
                lock (LockObj)
                {
                    return (from kv in BufferTasks
                            select kv.Value).ToArray();
                }
            }
        }

        /// <summary>
        /// 获取一个任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        private static Task SafeGetTask(int taskId)
        {
            lock (LockObj)
            {
                return (from kv in BufferTasks
                        where kv.Value.TaskId.Equals(taskId)
                        select kv.Value).FirstOrDefault();
            }
        }
    }
}
