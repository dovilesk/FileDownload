using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace FileDownloadApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

         public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
         WebHost.CreateDefaultBuilder(args)
         .ConfigureKestrel(options =>
         {
             options.ListenLocalhost(1234, listenOptions =>
             {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                 listenOptions.UseHttps("dovilescert.pfx", "testtest");
             });
         })
        .UseStartup<Startup>();

    //    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    //WebHost.CreateDefaultBuilder(args)
    //    .UseStartup<Startup>();
    }
}
