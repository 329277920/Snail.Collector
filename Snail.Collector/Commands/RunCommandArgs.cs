using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.Commands
{
    public class RunCommandArgs
    {
        private int _collectId;
        public int CollectId
        {
            get { return this._collectId; }
        }
     
        public bool Success { get; private set; }

        public string Error { get; private set; }

        public void Parse(params string[] args)
        {
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
                    case "-id":
                        if (!int.TryParse(value, out this._collectId))
                        {
                            this.Success = false;
                            this.Error = "采集编号必须是数字";
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
