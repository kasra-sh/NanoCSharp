using Microsoft.Extensions.DependencyInjection;

namespace Nano.Core.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureNanoServices(this IServiceCollection serviceCollection)
        {
            NanoEngineContext.SetServiceCollection(serviceCollection);
        }
    }
}
