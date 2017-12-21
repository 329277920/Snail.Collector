using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static Random _rand = new Random();

        static void Main(string[] args)
        {
            // var url = "http://devsvc.manjd.com/account/stores/201";
            var url = "http://www.baidu.com";

            var urls = new string[] { "http://www.baidu.com", "http://devsvc.manjd.com/account/stores/201" };

            Stopwatch watch = new Stopwatch();
            watch.Start();
            int count = 0;

            new System.Threading.Tasks.TaskFactory().StartNew(()=> 
            {
                var preCount = 0;
                while (true)
                {
                    Console.WriteLine("{0},{1}",count - preCount, count);
                    preCount = count;
                    System.Threading.Thread.Sleep(1000);
                }                
            });
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(new HttpClientHandler() { UseCookies = false });
            using (var parallel = new Snail.Sync.Parallel(100))
            {
                parallel.ForEach<int>(new int[100000], (item) =>
                {
                    try
                    {
                       
                        client.GetAsync(url).ConfigureAwait(false).GetAwaiter().GetResult();
                        lock (url)
                        {
                            count++;                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                        
                });
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.ReadKey();


        }
    }
}
