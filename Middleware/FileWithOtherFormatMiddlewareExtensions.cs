using Microsoft.AspNetCore.Builder;

namespace FileDownloadApp.Middleware
{

    public static class FileWithOtherFormatMiddlewareExtensions
    {
        public static IApplicationBuilder UseOtherFormat(this IApplicationBuilder app)
        {
            return app.UseMiddleware<FileWithOtherFormatMiddleware>();
        }
    }
}
