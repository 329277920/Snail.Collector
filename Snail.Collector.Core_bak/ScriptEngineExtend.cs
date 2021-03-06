﻿using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snail.IO;
using System.Reflection;
using Microsoft.ClearScript;
using Snail.Collector.Core.SystemModules;
using Snail.Collector.JSAdapter;
using Snail.Collector.Storage;

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
            v8.AddHostObject("lib", new HostTypeCollection("mscorlib", "System.Core"));
            v8.AddHostObject("host", new HostModuleExtend());
            v8.AddHostType("Array", typeof(JSArray));
            v8.AddHostObject("http", new HttpModuleExtend());                        
            var moduleProxy = Unity.ReadResource("Snail.Collector.Core.SystemModules.SystemModule.js");
            if (!string.IsNullOrEmpty(moduleProxy))
            {
                v8.Execute(moduleProxy);
            }
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
            var ass = Assembly.LoadFile(Path.Combine(module.ExecutePath, module.Assembly));
            if (ass == null)
            {
                throw new Exception("the path is not found,'" + module.Assembly + "'.");
            }
            var type = ass.GetType(module.Type);
            if (type == null)
            {
                throw new Exception("the type is not found,'" + module.Type + "'.");
            }
            var refType = string.Format("_ref_module_type_{0}", type.Name);
            v8.AddHostType(refType, type);
            var funcName = string.Format("_init_module_func_{0}", module.Name);
            var script = new StringBuilder("function ");
            script.Append(funcName + "() {\r\n");
            script.AppendFormat("var {0} = new {1}();\r\n", module.Name, refType);
            if (module.ProxyScript?.Length > 0)
            {
                script.Append(module.ProxyScript);
            }
            script.AppendFormat("\r\nreturn {0};", module.Name);
            script.Append("\r\n}");
            v8.Execute(script.ToString());
            return v8.Invoke(funcName);
        }
        
    }
}

