
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Snail.IO;
using System.IO;
using Snail.Data;
using Snail.Collector.Core.Configuration;
using Snail.Collector.Common;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 模块管理器   
    /// </summary>
    internal sealed class ModuleMamanger
    {
        private const string LogSource = "modules";

        /// <summary>
        /// 查找指定名称的模块定义
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static ModuleInfo FindModule(string module)
        {
            lock (ExtendModules)
            {
                return ExtendModules.SingleOrDefault(m => m.Name.Equals(module));
            }
        }

        #region 私有成员
       
        /// <summary>
        /// 存储扩展模块列表
        /// </summary>
        private static List<ModuleInfo> ExtendModules;

        private static List<string> ExecutePaths;

        /// <summary>
        /// 静态初始化，加载所有系统模块
        /// </summary>
        static ModuleMamanger()
        {
            InitExtendModules();
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                if (e.RequestingAssembly == null)
                {
                    return null;
                }
                List<string> dirList = new List<string>();
                if (e.RequestingAssembly != null)
                {
                    dirList.Add(e.RequestingAssembly.Location.Substring(0,
                        e.RequestingAssembly.Location.LastIndexOf("\\") + 1));                                   
                }
                else
                {
                    dirList = ExecutePaths;                   
                }
                lock (ExtendModules)
                {
                    foreach (var dir in dirList)
                    {
                        var ass = FindAssembly(dir, e.Name);
                        if (ass != null)
                        {
                            return ass;
                        }
                    }
                }
                throw new Exception("未能找到程序集:" + e.Name);
            };
        }       

        /// <summary>
        /// 初始化扩展模块
        /// </summary>
        private static void InitExtendModules()
        {
            ExtendModules = new List<ModuleInfo>();
            ExecutePaths = new List<string>();
            var modulePath = PathUnity.GetFullPath("modules");
            if (modulePath?.Length > 0)
            {
                foreach (var dir in Directory.GetDirectories(modulePath))
                {
                    var file = Directory.GetFiles(dir, "module.json").SingleOrDefault();
                    if (string.IsNullOrEmpty(file))
                    {
                        continue;
                    }
                    var content = file.ReadToEnd(ConfigManager.DefulatEncoding);
                    if (string.IsNullOrEmpty(content))
                    {
                        continue;
                    }
                    var config = Serializer.JsonDeserialize<ModuleInfo>(content);                    
                    config.ExecutePath = dir;
                    if (config.ProxyScript?.Length > 0)
                    {
                        if (File.Exists(Path.Combine(dir, config.ProxyScript)))
                        {
                            config.ProxyScript = new FileInfo(Path.Combine(dir, config.ProxyScript)).FullName.ReadToEnd(ConfigManager.DefulatEncoding);
                        }
                    }
                    ExtendModules.Add(config);
                    if (!ExecutePaths.Contains(dir))
                    {
                        ExecutePaths.Add(dir);
                    }                   
                    //AppDomain.CurrentDomain.SetData("PRIVATE_BINPATH", "Modules/html");
                    //// AppDomain.CurrentDomain.SetData("BINPATH_PROBE_ONLY", @"C:\Projects\Git\Snail.Collector\Snail.Collector\bin\Debug\Modules\html");
                    //var m = typeof(AppDomainSetup).GetMethod("UpdateContextProperty", BindingFlags.NonPublic | BindingFlags.Static);
                    //var funsion = typeof(AppDomain).GetMethod("GetFusionContext", BindingFlags.NonPublic | BindingFlags.Instance);
                    //m.Invoke(null, new object[] { funsion.Invoke(AppDomain.CurrentDomain, null), "PRIVATE_BINPATH", @"C:\Projects\Git\Snail.Collector\Snail.Collector\bin\Debug\Modules\html" });



                }
            }
        }

        /// <summary>
        /// 获取扩展模块执行路径
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static string GetModuleExecutePath(string assembly)
        {
            lock (ExtendModules)
            {
                return (from item in ExtendModules
                        where item.Assembly.Equals(assembly)
                        select item.ExecutePath).FirstOrDefault();
            }
        }


        private static Assembly FindAssembly(string dir, string assFullName)
        {
            foreach (var file in new DirectoryInfo(dir).GetFiles("*.dll"))
            {
                try
                {
                    var ass = Assembly.ReflectionOnlyLoadFrom(file.FullName);
                    if (ass.FullName == assFullName)
                    {
                        return Assembly.LoadFile(file.FullName);
                    }
                }
                catch (Exception ex)
                {
                    LoggerProxy.Error(LogSource, string.Format("call FindAssembly error.dir is '{0}', assName is '{1}'.", dir, assFullName), ex);
                }
            }
            return null;
        }
        
        #endregion
    }
}
