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
        /// <summary>
        /// 采集任务id
        /// </summary>
        public int CollectId
        {
            get { return this._collectId; }
        }

        private int _repeat;
        /// <summary>
        /// 重复执行次数，默认1
        /// </summary>
        public int Repeat
        {
            get { return this._repeat; }
        }

        private TimeSpan _delay;
        public TimeSpan Delay
        {
            get { return this._delay; }
        }

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
            this._repeat = 1;
            this._delay = TimeSpan.Parse("00:00:00");
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
                    case "-repeat":
                        if (!int.TryParse(value, out this._repeat))
                        {
                            this.Success = false;
                            this.Error = "重复执行次数必须为数字";
                            return;
                        }
                        break;
                    case "-delay":
                        if (!TimeSpan.TryParse(value, out this._delay))
                        {
                            this.Success = false;
                            this.Error = "延迟执行参数格式:'00:00:10'";
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
