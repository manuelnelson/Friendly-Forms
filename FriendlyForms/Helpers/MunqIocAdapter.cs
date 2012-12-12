using Munq.MVC3;
using ServiceStack.Configuration;

namespace FriendlyForms.Helpers
{
    public class MunqIocAdapter : IContainerAdapter
    {
        private Munq.IocContainer Container { get; set; }
        public MunqIocAdapter()
        {
            Container = MunqDependencyResolver.Container;
        }
        public T Resolve<T>()
        {
            return (T)Container.Resolve(typeof(T)); //Resolve<T>();
        }

        public T TryResolve<T>()
        {
            if (Container.CanResolve(typeof(T)))
                return (T)Container.Resolve(typeof(T)); //Resolve<T>();
            return default(T);
        }
    }
}