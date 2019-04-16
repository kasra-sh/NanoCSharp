using System.Security.Permissions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Nano.Web
{
    public class Program
    {
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options => options.ListenAnyIP(8000))
                .UseStartup<Startup>();
    }
}
