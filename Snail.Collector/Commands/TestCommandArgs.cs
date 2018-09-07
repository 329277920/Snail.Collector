using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Commands
{
    /// <summary>
    /// 测试命令参数
    /// </summary>
    public class TestCommandArgs
    {         
        public string ScriptFilePath { get; private set; }

        public bool Success { get; private set; }

        public string Error { get; private set; }

        public void Parse(params string[] args)
        {
            this.Success = true;
            if (args.Length <= 0)
            {
                this.Success = false;
                return;
            }
            for (var i = 0; i < args.Length; i++)
            {
                var tag = args[i];
                if (++i >= args.Length)
                {
                    this.Success = false;
                    return;
                }
                var value = args[i];
                switch (tag)
                {                     
                    case "-file":
                        this.ScriptFilePath = value;
                        if (string.IsNullOrEmpty(this.ScriptFilePath))
                        {
                            this.Success = false;
                            this.Error = "应提供一个采集开始脚本文件";
                            return;
                        }
                        if (!File.Exists(this.ScriptFilePath))
                        {
                            this.Success = false;
                            this.Error = $"未找到路径'{this.ScriptFilePath}'";
                            return;
                        }
                        break;
                    default:
                        this.Success = false;
                        return;
                }
            }
        }
    }
}
