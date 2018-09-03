using Snail.Collector.Commands;
using Snail.Collector.Core;
using Snail.Collector.Log;
using Snail.Collector.Repositories;
using Unity;

namespace Snail.Collector.Common
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
            _container.RegisterSingleton<ILogger, Log4NetLogger>();
            _container.RegisterSingleton<ICommand, AddCommand>("task_add");
            _container.RegisterSingleton<ICommand, RunCommand>("task_run");
            _container.RegisterType<CollectTaskAccessProxy>();             
            _container.RegisterSingleton<CollectTaskRuntime, CollectTaskRuntime>();
        }       

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public static T Resolve<T>(string name)
        {
            return _container.Resolve<T>(name);
        }
    }
}
