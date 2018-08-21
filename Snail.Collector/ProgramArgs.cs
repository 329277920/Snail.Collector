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
        /// <summary>
        /// 当前执行的任务
        /// </summary>
        public string Command { get; set; }

        public Dictionary<string, string> Options { get; set; }

        public (bool result, string error) Init(params string[] args)
        {
            if (args == null || args.Length <= 0)
            {
                return (false, null);
            }
            var idx = 0;           
            switch (args[idx++])
            {
                case "run":
                    for (var i = idx; i < args.Length; i++)
                    {

                    }
                    break;
                case "add":
                    break;
                default:
                    return (false, null);
                    break;
            }
        }       
    }
}
