﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Snail.Collector.Modules
{
    public class DebugModule
    {
        public void writeLine(object value)
        {
            Console.WriteLine($"{DateTime.Now} - {value?.ToString()}");         
        }
    }
}
