using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Snail.Collector.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {                       
            appBuilder.UseWebApi(InitWebApi());
            // appBuilder.Run(HandleRequest);
        }

        public HttpConfiguration InitWebApi()
        {
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",//其中action指的就是方法名,这种方式可以直接按http://localhost:9000/api/valuesparam/getproduct的方式访问  
               defaults: new { id = RouteParameter.Optional }//Optional表明routeTemplate中的id是可选的  
            );

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            ////默认返回 json  
            config.Formatters
                .JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("datatype", "json", "application/json"));
            //返回格式选择  
            config.Formatters
                .XmlFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("datatype", "xml", "application/xml"));
            //json 序列化设置  
            config.Formatters
                .JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
                {
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                };
            return config;
        }

        //static Task HandleRequest(IOwinContext context)
        //{            
        //    context.Response.ContentType = "text/plain";
        //    return context.Response.WriteAsync("Hello, world!");
        //}
    }
}
