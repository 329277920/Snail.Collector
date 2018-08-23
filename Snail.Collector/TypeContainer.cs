using Snail.Collector.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Snail.Collector
{
    /// <summary>
    /// 类型容器
    /// </summary>
    public sealed class TypeContainer
    {
        private static UnityContainer _container;
        static TypeContainer()
        {
            SqliteProxy.Init("data/db.sqlite");

            _container = new UnityContainer();
            _container.RegisterSingleton<ICollectRepository, CollectRepository>();
            _container.RegisterSingleton<ICollectTaskRepository, CollectTaskRepository>();
        }       

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
