﻿using Snail.Collector.Core.Configuration;
using Snail.Collector.Storage;
using Snail.Collector.Storage.DB;
using Snail.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 任务存储对象
    /// </summary>
    public class TaskItems
    {        
        public static TaskItems Instance = new Lazy<TaskItems>(() =>
        {
            return new TaskItems(new DbProviderConfig
            {
                Driver = StorageProviders.Sqlite,
                Connection = "Data Source=" + ConfigManager.Current.DatabaseFilePath + ";Version=3;"
            });
        }, true).Value;
        
        private IStorageProvider _db;

        private string _tbName = "TaskItems";

        private TaskItems(DbProviderConfig cfg)
        {
            this._db = new DbProvider(cfg);
        }

        /// <summary>
        /// 添加一个任务
        /// </summary>
        /// <param name="task">任务配置{ url:xx,script:xx }</param>
        /// <returns>返回是否添加成功</returns>
        public bool AddObj(object task)
        {
            lock (this) {
                return this._db.Insert(this._tbName, task) > 0;
            }
           
        }        

        /// <summary>
        /// 添加一个任务
        /// </summary>
        /// <param name="task">任务对象</param>
        /// <returns>返回是否添加成功</returns>
        public bool Add(TaskItemEntity task)
        {
            lock (this)
            {
                return this._db.Insert(this._tbName, new
                {
                    parentId = task.ParentId,
                    script = task.Script,
                    taskId = task.TaskId,
                    url = task.Url,
                }) > 0;
            }
           
        }

        /// <summary>
        /// 添加任务的最上层执行者,如果存在，不继续添加
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool AddRoot(TaskItemEntity task)
        {
            lock (this)
            {
                var taskInfo = this.Get(new { taskId = task.TaskId });
                if (taskInfo != null)
                {
                    return true;
                }
                return this.Add(task);
            }
          
        }

        /// <summary>
        /// 按条件获取一个任务
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public TaskItemEntity Get(object filter)
        {
            lock (this)
            {
                return this._db.SelectSingle<TaskItemEntity>(this._tbName, filter);
            }
            
        }

        /// <summary>
        /// 获取一个未执行的任务，并将此任务置为执行状态
        /// </summary>
        /// <param name="taskId">所属任务Id</param>
        /// <returns></returns>
        public TaskItemEntity GetExec(int taskId)
        {
            lock (this)
            {
                try
                {
                    var filter = "{ \"taskId\" : " + taskId + " , \"status\" : { \"$in\" : [0,2] } }";

                    var taskInfo = this._db.SelectSingle<TaskItemEntity>(this._tbName, Serializer.JsonDeserialize(filter));
                    if (taskInfo != null)
                    {
                        taskInfo.Status = 1;
                        taskInfo.ExecCount++;
                        taskInfo.ExecTime = DateTime.Now;
                        lock (this)
                        {
                            return this._db.Update(this._tbName, new { Id = taskInfo.Id }, taskInfo) == 1 ? taskInfo : null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                    // todo: 写日志                 
                }
                return null;
            }
            
        }

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(TaskItemEntity entity)
        {           
            lock (this)
            {
                try
                {
                    return this._db.Update(this._tbName, new
                    {
                        id = entity.Id
                    }, entity) == 1;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                    // todo: 写日志                 
                }
                return false;
            }            
        }        
    }
}
