
using Microsoft.ClearScript;
using System;
using System.Threading.Tasks;

namespace Snail.Collector.Core.Modules
{
    public static class HostModuleExtend
    {                               
        /// <summary>
        /// 加载指定的模块到JS运行环境中
        /// </summary>
        /// <param name="module">模块名称</param>        
        public static void Import(this ExtendedHostFunctions host, string module)
        {
            CollectorFactory.Current.ScriptEngine.LoadModule(module);            
        }

        public static async Task InfoAsync(this ExtendedHostFunctions host, string content, Action callBack)
        {            
            await new TaskFactory().StartNew((msg) =>
            {
                System.Diagnostics.Debug.WriteLine(msg as string);

                System.Threading.Thread.Sleep(1000);
            }, content);

            callBack?.Invoke();
        }

        public static async Task InfoAsync2(this ExtendedHostFunctions host, string content, Func<bool,string> callBack)
        {
            await new TaskFactory().StartNew((msg) =>
            {
                System.Diagnostics.Debug.WriteLine(msg as string);

                System.Threading.Thread.Sleep(1000);
            }, content);

            var returnMsg = callBack?.Invoke(true);
            System.Diagnostics.Debug.WriteLine(returnMsg);        
        }

        public static void Info(this ExtendedHostFunctions host, string content)
        {
            System.Diagnostics.Debug.WriteLine(content);
        }

        /// <summary>
        /// 引用一个脚本文件
        /// </summary>
        /// <param name="scriptFilePath">脚本文件路径</param>
        public static void reference(this ExtendedHostFunctions host, string scriptFilePath)
        {

        }

        public static void Test(this ExtendedHostFunctions host, object obj)
        {

        }
    }
}
