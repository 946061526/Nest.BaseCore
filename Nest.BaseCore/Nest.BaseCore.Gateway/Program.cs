using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Nest.BaseCore.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
          .ConfigureAppConfiguration((hostingContext, builder) =>
          {
              builder
              .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
              .AddJsonFile("Ocelot.json", true, true);
          })
          .UseStartup<Startup>();
    }
}
