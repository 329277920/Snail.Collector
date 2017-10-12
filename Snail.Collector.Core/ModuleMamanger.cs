
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Snail.IO;
using System.IO;
using Snail.Seriazliation;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 模块管理器
    /// </summary>
    internal sealed class ModuleMamanger
    {
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

        /// <summary>
        /// 静态初始化，加载所有系统模块
        /// </summary>
        static ModuleMamanger()
        {
            InitExtendModules();           
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) => 
            {
                return null;
                //e.Name
                //string strFielName = args.Name.Split(',')[0];
                //return Assembly.LoadFile(string.Format(@"C:\test\{0}.dll", strFielName));
            };
        }

        /// <summary>
        /// 初始化系统内置模块
        /// </summary>
        //private static void InitSystemModules()
        //{            
        //    SystemModules = new List<ModuleInfo>();
        //    var jsonDefine = ReadResource("Snail.Collector.Core.Modules.systemModule.json");
        //    var modules = Seriazliation.Serializer.JsonDeserialize<List<ModuleInfo>>(jsonDefine);
        //    if (modules?.Count > 0)
        //    {                 
        //        modules.ForEach(item =>
        //        {
        //            item.Assembly = PathUnity.GetFullPath(item.Assembly);
        //            if (item.ProxyScript?.Length > 0)
        //            {
        //                item.ProxyScript = ReadResource(item.ProxyScript);
        //            }                    
        //        });
        //        SystemModules.AddRange(modules.ToArray());
        //    }
        //}

        /// <summary>
        /// 初始化扩展模块
        /// </summary>
        private static void InitExtendModules()
        {
            ExtendModules = new List<ModuleInfo>();
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
                    var content = new FileInfo(file).ReadStringAsync(Encoding.UTF8).Result;
                    if (string.IsNullOrEmpty(content))
                    {
                        continue;
                    }
                    var config = Serializer.JsonDeserialize<ModuleInfo>(content);
                    config.Assembly = Path.Combine(dir, config.Assembly);
                    if (config.ProxyScript?.Length > 0)
                    {
                        if (File.Exists(Path.Combine(dir, config.ProxyScript)))
                        {
                            config.ProxyScript = new FileInfo(Path.Combine(dir, config.ProxyScript)).ReadStringAsync(Encoding.UTF8).Result;
                        }
                    }
                    ExtendModules.Add(config);


                    //AppDomain.CurrentDomain.SetData("PRIVATE_BINPATH", "Modules/html");
                    //// AppDomain.CurrentDomain.SetData("BINPATH_PROBE_ONLY", @"C:\Projects\Git\Snail.Collector\Snail.Collector\bin\Debug\Modules\html");
                    //var m = typeof(AppDomainSetup).GetMethod("UpdateContextProperty", BindingFlags.NonPublic | BindingFlags.Static);
                    //var funsion = typeof(AppDomain).GetMethod("GetFusionContext", BindingFlags.NonPublic | BindingFlags.Instance);
                    //m.Invoke(null, new object[] { funsion.Invoke(AppDomain.CurrentDomain, null), "PRIVATE_BINPATH", @"C:\Projects\Git\Snail.Collector\Snail.Collector\bin\Debug\Modules\html" });



                }
            }
        }
        
        #endregion
    }
}
