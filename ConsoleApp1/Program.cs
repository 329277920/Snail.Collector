using MJD.Framework.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MJD.Framework.CrossCutting;

namespace ConsoleApp1
{
    class Program
    {
        private static Random _rand = new Random();

        static void Main(string[] args)
        {
            // var url = "http://devsvc.manjd.com/account/stores/201";
            // var url = "http://192.168.10.82:9020/shop/m/test";

            // var urls = new string[] { "http://www.baidu.com", "http://devsvc.manjd.com/account/stores/201" };
            // var url = "http://devsvc.manjd.com/account/stores/201";
            var url = "http://myapi.manjd.com/common/m/test";
            // var url = "http://192.168.10.82:9020/shop/m/getShop";
            // var url = "http://devapi.manjd.com/api/shop/m/getShop";
            // var url = "http://checksvc.manjd.com/account/public/601";
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

            FlurlClient client = new FlurlClient(url);
            client.ConfigureHttpClient(http =>
            {
                http.DefaultRequestHeaders.Add("shopid", "6");
                http.DefaultRequestHeaders.Add("sharesource", "1");
            });

            using (var parallel = new Snail.Sync.Parallel(50))
            {
                parallel.ForEach<int>(new int[500000], (item) =>
                {
                    try
                    {
                        //System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(new HttpClientHandler() { UseCookies = false });
                        //client.DefaultRequestHeaders.Add("shopid", "6");
                        //client.DefaultRequestHeaders.Add("sharesource", "1");
                        //client.GetAsync(url).ConfigureAwait(false).GetAwaiter().GetResult().Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                        //using (FlurlClient client = new FlurlClient(url))
                        //{
                        var obj = client.PostStringAsync("").ReceiveString().ConfigureAwait(true).GetAwaiter().GetResult().ToObject<dynamic>();
                        // }
                        lock (url)
                        {
                            count++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    System.Threading.Thread.Sleep(10);

                });
            }
            watch.Stop();
            Console.WriteLine("结束,耗时:"  + watch.ElapsedMilliseconds);
            Console.ReadKey();


        }
    }
}
