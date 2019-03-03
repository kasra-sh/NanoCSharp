using System;
using Microsoft.Extensions.DependencyInjection;

namespace Nano.Core.IoC
{
    public class NanoEngineContext
    {
        private static IServiceCollection _serviceCollection = null;

        private static IServiceProvider _serviceProvider = null;

        public static void SetServiceCollection(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public static IServiceCollection ServiceCollection
        {
            get
            {
                if (_serviceCollection == null) throw new Exception("IServiceCollection not initialized!");
                return _serviceCollection;
            }
        }

        public static IServiceProvider Current => _serviceProvider;

        public static IServiceProvider New => _serviceCollection.BuildServiceProvider();
    }
}
