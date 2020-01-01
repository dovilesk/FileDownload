using Microsoft.AspNetCore.Builder;

namespace FileDownloadApp.Middleware
{
    public static class FileWithSupportedFormatMiddlewareExtensions
    {
        public static IApplicationBuilder UseSupportedFormat(this IApplicationBuilder app)
        {
            return app.UseMiddleware<FileWithSupportedFormatMiddleware>();
        }
    }
}
