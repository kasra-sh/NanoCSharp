using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Nano.Core.IoC;

namespace Nano.Core.Extensions
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
