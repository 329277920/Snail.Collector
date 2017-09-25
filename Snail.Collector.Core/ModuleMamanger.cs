
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Snail.IO;

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
        public static ModuleDefine FindModule(string module)
        {
            lock (ExtendModules)
            {
                return ExtendModules.SingleOrDefault(m => m.Name.Equals(module));
            }
        }

        #region 私有成员

        /// <summary>
        /// 存储系统内置模块
        /// </summary>
        internal static List<ModuleDefine> SystemModules;

        /// <summary>
        /// 存储扩展模块列表
        /// </summary>
        private static List<ModuleDefine> ExtendModules;

        /// <summary>
        /// 静态初始化，加载所有系统模块
        /// </summary>
        static ModuleMamanger()
        {            
            InitSystemModules();

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
        private static void InitSystemModules()
        {            
            SystemModules = new List<ModuleDefine>();
            var jsonDefine = ReadResource("Snail.Collector.Core.Modules.systemModule.json");
            var modules = Seriazliation.Serializer.JsonDeserialize<List<ModuleDefine>>(jsonDefine);
            if (modules?.Count > 0)
            {                 
                modules.ForEach(item =>
                {
                    item.Assembly = PathUnity.GetFullPath(item.Assembly);
                    if (item.ProxyScript?.Length > 0)
                    {
                        item.ProxyScript = ReadResource(item.ProxyScript);
                    }                    
                });
                SystemModules.AddRange(modules.ToArray());
            }
        }

        /// <summary>
        /// 初始化扩展模块
        /// </summary>
        private static void InitExtendModules()
        {

        }

        /// <summary>
        /// 读取资源文件
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        private static string ReadResource(string resourceName)
        {
            return
                Assembly.GetExecutingAssembly().
                GetManifestResourceStream(resourceName).ReadToEndAsync(Encoding.UTF8).Result;
        }
                  
        #endregion
    }
}
