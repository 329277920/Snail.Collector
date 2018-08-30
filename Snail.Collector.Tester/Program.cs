using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Snail.Collector.Modules;
using Snail.Collector.Modules.Http;
using Snail.Collector.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var script = "";
            string func = "___func";
            using (FileStream fs = new FileStream("test.js", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    script = sr.ReadToEnd();
                }
            }
            script = FormatScript(script, func);
            var scriptEngine = CreateScriptEngine();
            scriptEngine.Execute(script);
            Console.WriteLine(scriptEngine.Invoke(func));
            Console.WriteLine("完成.");
            Console.ReadKey();
        }

        static string FormatScript(string script,string func)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append($"function {func}(){{ \r\n");
            buffer.Append("try \r\n { \r\n");
            buffer.Append($"{script} \r\n }} \r\n");
            buffer.Append("catch (err) { \r\n");
            buffer.Append("debug.writeLine(err.message); \r\n ");
            buffer.Append("log.error(err.message); \r\n } \r\n");
            buffer.Append("}");
            return buffer.ToString();
        }

        static V8ScriptEngine CreateScriptEngine()
        {
            var scriptEngine = new V8ScriptEngine();

            scriptEngine.AddHostObject("lib", new HostTypeCollection("mscorlib", "System.Core"));
            scriptEngine.AddHostObject("debug", new DebugModule());
            scriptEngine.AddHostObject("log", new LoggerModule());
            scriptEngine.AddHostObject("http", new HttpModule());
            scriptEngine.AddHostType("Array", typeof(JSArray));
            scriptEngine.Execute(ResourceManager.ReadResource("Snail.Collector.JsExtends.stringExtend.js"));
            scriptEngine.Execute(ResourceManager.ReadResource("Snail.Collector.JsExtends.unity.js"));

            return scriptEngine;
        }
    }
}
