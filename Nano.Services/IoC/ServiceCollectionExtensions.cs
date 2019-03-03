using Microsoft.Extensions.DependencyInjection;

namespace Nano.Services.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureNanoServices(this IServiceCollection serviceCollection)
        {
            NanoEngineContext.SetServiceCollection(serviceCollection);
        }
    }
}
