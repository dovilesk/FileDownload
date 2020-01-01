using FileDownloadApp.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FileDownloadApp.Middleware
{
    public class FileWithOtherFormatMiddleware
    {
        private readonly RequestDelegate _next;

        public FileWithOtherFormatMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var fileName = context.Request.Path.ToString();

            if (fileName.Contains("."))
            {
                context.Response.Headers.Add("Accept", String.Join(",", MimeTypes.GetMimeTypes().Values.ToArray()));
                context.Response.Headers.Add("Host", context.Request.Host.ToString());
                context.Response.StatusCode = StatusCodes.Status406NotAcceptable;
            }
            else
            {
                await _next.Invoke(context);
            }
        }

    }
}
