using System.Threading;
using System.Threading.Tasks;

namespace Snail.Collector.Core.Modules
{
    
    public class LogModule
    {
        public static void info(string content)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(content);
#endif
        }

        public static async Task infoAsync(string content, object callBack)
        {
            await new TaskFactory().StartNew(() =>
            {
                System.Diagnostics.Debug.WriteLine(content);
                Thread.Sleep(1000);
                                                
            });
            var ss = callBack;
            // callBack?.Invoke();
        }
    }
}
