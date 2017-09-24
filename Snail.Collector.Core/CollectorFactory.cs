using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 采集器工厂
    /// </summary>
    public class CollectorFactory
    {
        /// <summary>
        /// 记录已经建立的采集器对象
        /// </summary>
        public static Snail.Data.SafeDictionary<string,Collector> CollectorList { get; private set; }

        static CollectorFactory()
        {
            CollectorList = new Data.SafeDictionary<string, Core.Collector>();
        }

        /// <summary>
        /// 建立一个新的采集器对象，并开始采集
        /// <param name="name">指定一个唯一名称</param>
        /// <param name="code">指定任务运行的脚本</param>
        /// </summary>         
        public static void StartNew(string name, string code)
        {
            if (name?.Length <= 0)
            {
                throw new ArgumentNullException("name");
            }
            if (code?.Length <= 0)
            {
                throw new ArgumentNullException("code");
            }
            var exists = false;
            if (!CollectorList.TryContainsKey(name, out exists))
            {
                throw new Exception("access collector list failed.");
            }
            if (exists)
            {
                throw new Exception("a collector named \"" + name + "\" already exists");
            }
            var collector = new Collector(name, code);

            if (!CollectorList.TryAdd(name, collector, false))
            {
                throw new Exception("failed to create collector.");
            }
            InnerStart(collector);
        }

        /// <summary>
        /// 指定一个任务脚本文件，启动采集
        /// </summary>
        /// <param name="collector"></param>
        public static void InnerStart(Collector collector)
        {
            var task = new System.Threading.Tasks.TaskFactory().StartNew((_c) =>
            {
                try
                {
                    var ct = (Collector)_c;
                    Current = ct;
                    ct.Start();                    
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("任务异常:" + ex.ToString());                   
                }               
            }, collector);            
        }

        /// <summary>
        /// 获取当前线程执行环境绑定的采集器对象
        /// </summary>
        public static Collector Current
        {
            get
            {
                return CallContext.GetData(Context_CacheKey) as Collector;
            }            
            private set
            {
                CallContext.SetData(Context_CacheKey, value);
            }
        }

        #region 私有成员

        private const string Context_CacheKey = "ScriptEngineContext";

        #endregion
    }
}
