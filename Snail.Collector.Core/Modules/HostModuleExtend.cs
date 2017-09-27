
using Microsoft.ClearScript;
using System;
using System.Threading.Tasks;

namespace Snail.Collector.Core.Modules
{
    public class HostModuleExtend : ExtendedHostFunctions
    {                               
        /// <summary>
        /// 加载指定的模块到JS运行环境中
        /// </summary>
        /// <param name="module">模块名称</param>        
        public void Import(string module)
        {
            CollectorFactory.Current.ScriptEngine.LoadModule(module);            
        }

        public async Task InfoAsync(string content, Action callBack)
        {            
            await new TaskFactory().StartNew((msg) =>
            {
                System.Diagnostics.Debug.WriteLine(msg as string);

                System.Threading.Thread.Sleep(1000);
            }, content);

            callBack?.Invoke();
        }

        public async Task InfoAsync2(string content, Func<bool,string> callBack)
        {
            await new TaskFactory().StartNew((msg) =>
            {
                System.Diagnostics.Debug.WriteLine(msg as string);

                System.Threading.Thread.Sleep(1000);
            }, content);

            var returnMsg = callBack?.Invoke(true);
            System.Diagnostics.Debug.WriteLine(returnMsg);        
        }

        public void Info(string content)
        {
            System.Diagnostics.Debug.WriteLine(content);
        }

        /// <summary>
        /// 引用一个脚本文件
        /// </summary>
        /// <param name="scriptFilePath">脚本文件路径</param>
        public void reference(string scriptFilePath)
        {

        }

        public void TestObj(dynamic user)
        {
            var str = user.name.ToString();

            WriteLine(user.name);
        }

        public void Test(dynamic obj)
        {
           
        }

        private void WriteLine(string content)
        {
            System.Diagnostics.Debug.WriteLine(content);
        }
    }

    public class UserInfo
    {
        public string name { get; set; }
    }
}
