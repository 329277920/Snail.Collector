
using System;
using System.Threading.Tasks;

namespace Snail.Collector.Core.Modules
{
    public class HostModule
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
            await new System.Threading.Tasks.TaskFactory().StartNew((msg) =>
            {
                System.Diagnostics.Debug.WriteLine(msg as string);

                System.Threading.Thread.Sleep(1000);
            }, content);

            callBack?.Invoke();
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

        
    }
}
