using System;


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
