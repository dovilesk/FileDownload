using FileDownloadApp.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace FileDownloadApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddCors();
            services.AddRouting();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //allow just request from this url
            app.UseCors(builder =>
            {
                builder.WithOrigins("https://localhost:1234");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(string.Empty, context =>
                {
                    return context.Response.WriteAsync($"Welcome to the default page!");
                });

                routes.MapMiddlewareGet("{name}", builder =>
                {
                    builder.UseSupportedFormat();
                    builder.UseOtherFormat();
                });

            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Application executes just GET requests from url https://localhost:1234/filename.extension");
            });

        }
    }
}
