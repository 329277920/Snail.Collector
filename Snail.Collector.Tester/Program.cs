using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Snail.Collector.Tester.Modules;
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
            string script = "";
            string func = "___func";
            using (FileStream fs = new FileStream("test.js", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    script = sr.ReadToEnd();
                }
            }
            var scriptEngine = CreateScriptEngine();
            scriptEngine.Execute($"function {func}()\r\n{{\r\n{script}\r\n}}");
            Console.WriteLine(scriptEngine.Invoke(func));
            Console.WriteLine("完成.");
            Console.ReadKey();
        }

        static V8ScriptEngine CreateScriptEngine()
        {
            var scriptEngine = new V8ScriptEngine();

            scriptEngine.AddHostObject("lib", new HostTypeCollection("mscorlib", "System.Core"));
            scriptEngine.AddHostObject("debug", new DebugModule());

            return scriptEngine;
        }
    }
}
