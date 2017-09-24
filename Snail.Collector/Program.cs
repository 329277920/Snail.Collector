using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Snail.Collector.Core;
using Snail.IO;

namespace Snail.Collector
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            var code = new System.IO.FileInfo(@"Tasks\first.js").ReadStringAsync(System.Text.Encoding.UTF8).Result;

            CollectorFactory.StartNew("cnf", code);

            // var engine = Core.ScriptEngineManager.NewScriptEngine();

            // engine.ExecuteFromFile("Tasks\\first.js", System.Text.Encoding.UTF8);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());

            
        }
    }
}
