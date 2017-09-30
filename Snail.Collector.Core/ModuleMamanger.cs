
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
        /// 初始化扩展模块
        /// </summary>
        private static void InitExtendModules()
        {
            ExtendModules = new List<ModuleInfo>();
            var mDir = PathUnity.GetDirPath("Modules");
            if (mDir?.Length > 0)
            {
                foreach (var dir in Directory.GetDirectories((mDir)))
                {
                    var mFile = Directory.GetFiles(dir, "module.json").SingleOrDefault();
                    if (mFile?.Length <= 0)
                    {
                        continue;
                    }
                    var mStr = ReadFileContent(mFile);
                    var mInfo = Serializer.JsonDeserialize<ModuleInfo>(mStr);
                    mInfo.Assembly = Path.Combine(dir, mInfo.Assembly);
                    if (mInfo.ProxyScript?.Length > 0)
                    {
                        var pFile = Path.Combine(dir, mInfo.ProxyScript);
                        mInfo.ProxyScript = ReadFileContent(pFile);
                    }
                    ExtendModules.Add(mInfo);
                }
            }           
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

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string ReadFileContent(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return fs.ReadToEndAsync(Encoding.UTF8).Result;
            }
        }

        #endregion
    }
}
