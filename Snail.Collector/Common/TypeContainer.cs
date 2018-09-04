using Microsoft.Extensions.Configuration;
using Snail.Collector.Commands;
using Snail.Collector.Core;
using Snail.Collector.Modules;
using Snail.Collector.Modules.Html;
using Snail.Collector.Modules.Http;
using Snail.Collector.Repositories;
using Unity;

namespace Snail.Collector.Common
{
    /// <summary>
    /// 类型容器
    /// </summary>
    public sealed class TypeContainer
    {
        internal static UnityContainer Container;
        static TypeContainer()
        {
            Container = new UnityContainer();
        }       

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static T Resolve<T>(string name)
        {
            return Container.Resolve<T>(name);
        }
    }
}
