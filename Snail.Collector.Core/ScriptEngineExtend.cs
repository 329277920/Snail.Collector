using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snail.IO;
using System.Reflection;

namespace Snail.Collector.Core
{
    /// <summary>
    /// 业务扩展 V8ScriptEngine
    /// </summary>
    public static class ScriptEngineExtend
    {        
        /// <summary>
        /// 加载系统扩展模块
        /// </summary>
        internal static void LoadSystemModules(this V8ScriptEngine v8)
        {
            ModuleMamanger.SystemModules.ForEach(item => v8.LoadModule(item));
        }

        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="v8"></param>
        /// <param name="module"></param>
        internal static void LoadModule(this V8ScriptEngine v8, string module)
        {
            var m = ModuleMamanger.FindModule(module);
            if (m == null)
            {
                throw new Exception("can not found a module the named '" + module + "'");
            }
            LoadModule(v8, m);
        }

        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="v8"></param>
        /// <param name="module"></param>
        internal static void LoadModule(this V8ScriptEngine v8, ModuleDefine module)
        {
            var ass = Assembly.LoadFile(module.Assembly);
            if (ass == null)
            {
                throw new Exception("the path is not found,'" + module.Assembly + "'.");
            }
            var type = ass.GetType(module.Type);
            if (type == null)
            {
                throw new Exception("the type is not found,'" + module.Type + "'.");
            }
            if (!module.Singleton)
            {
                v8.AddHostType(module.Name, type);
            }
            else
            {
                var instance = ass.CreateInstance(module.Type);
                v8.AddHostObject(module.Name, instance);
            }
            if (module.ProxyScript?.Length > 0)
            {
                v8.Execute(module.ProxyScript);
            }
        }

        /// <summary>
        /// 执行指定的脚本文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="coding">文件编码方式</param>
        public static void ExecuteFromFile(this V8ScriptEngine v8, string filePath, Encoding coding)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                v8.Execute(fs.ReadToEndAsync(coding).Result);
            }
        }

        
    }
}

