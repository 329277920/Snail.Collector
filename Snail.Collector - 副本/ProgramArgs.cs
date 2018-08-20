using System;
using System.Collections.Generic;
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

                        }
                        break;
                    case "-f":
                        break;
                }
            }           
        }
    }
}
