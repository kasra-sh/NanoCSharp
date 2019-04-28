using System;
using Microsoft.Extensions.DependencyInjection;

namespace Nano.Core.IoC
{
    public static class ServiceProviderExtensions
    {
        public static T Resolve<T>(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<T>();
        }

        public static void InitNanoEngine(this IServiceCollection serviceCollection)
        {
//            var sp = serviceCollection.BuildServiceProvider();
            NanoEngineContext.SetServiceCollection(serviceCollection);
        }
        
    }
}
