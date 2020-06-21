namespace PipelinesApp.Api
{
    using System.Net;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        ////.UseKestrel(options => {
                        ////        options.Listen(IPAddress.Loopback, 80);
                        ////    })
                        .UseStartup<Startup>();
                });
    }
}
