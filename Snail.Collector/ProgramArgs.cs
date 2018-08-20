using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector
{
    public class ProgramArgs
    {
        private int _collectId;
        public int CollectId { get { return _collectId; } }

        public string CollectName { get; set; }

        public string StartUri { get; set; }

        public string StartScriptFile { get; set; }

        public ProgramArgs(params string[] args)
        {
            if (args == null)
            {
                return;
            }
            for (var i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-id":
                        if (!int.TryParse(args[++i], out this._collectId))
                        {
                            throw new Exception("id需要一个整数");
                        }
                        break;
                    case "-name":
                        this.CollectName = args[++i];
                        break;
                    case "-file":
                        if (!File.Exists(args[++i]))
                        {
                            throw new Exception($"找不到脚本文件'{args[i]}'");
                        }
                        break;
                    case "-url":
                        this.StartUri = args[++i];
                        break;
                }
            }           
        }
    }
}
