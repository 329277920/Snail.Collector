using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Snail.Collector.Core.SystemModules;
using Snail.IO;
using System;
using System.IO;
using System.Reflection;
using System.Text;

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
            // 记载lib类型支持
            v8.AddHostObject("lib", new HostTypeCollection("mscorlib", "System.Core"));

            // 加载host，用于导入其他模块
            v8.AddHostObject("host", new HostModuleExtend());
        }

        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="v8"></param>
        /// <param name="module"></param>
        internal static object LoadModule(this V8ScriptEngine v8, string module)
        {
            var m = ModuleMamanger.FindModule(module);
            if (m == null)
            {
                throw new Exception("can not found a module the named '" + module + "'");
            }
            return LoadModule(v8, m);
        }

        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="v8"></param>
        /// <param name="module"></param>
        internal static object LoadModule(this V8ScriptEngine v8, ModuleInfo module)
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

            var typeName = "register_host_" + module.Name;
            var funcName = string.Format("initModule_{0}", module.Name);

            v8.AddHostType(typeName, type);

            var func = new StringBuilder(string.Format("function {0}(){{\r\n", funcName));
            func.Append(string.Format("var {0} = new {1}();\r\n", module.Name, typeName));
            if (module.ProxyScript?.Length > 0)
            {
                func.Append(module.ProxyScript);
            }
            func.Append(string.Format("\r\nreturn {0};}}", module.Name));

            v8.Execute(func.ToString());

            return v8.Invoke(funcName);
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

