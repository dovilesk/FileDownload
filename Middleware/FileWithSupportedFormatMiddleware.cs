using FileDownloadApp.Common;
using FileDownloadApp.Logic;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FileDownloadApp.Middleware
{
    public class FileWithSupportedFormatMiddleware
    {
        private readonly RequestDelegate _next;

        public FileWithSupportedFormatMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var fileName = context.Request.Path.ToString();

            if (FileTypeIsSupported(fileName))
            {
                FileDownload downloadFile = new FileDownload();
                var file = downloadFile.GetFile(fileName);

                if (file != null)
                {
                    GetHeaderContentDisposition(context, fileName);
                    await GetResponseBody(context, file);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        private static bool FileTypeIsSupported(string fileName)
        {
            var keys = MimeTypes.GetMimeTypes().Select(x => x.Key).ToArray();

            foreach (string keyValue in keys)
            {
                if (fileName.Contains(keyValue))
                {
                    return true;
                }
            }

            return false;
        }

        private static void GetHeaderContentDisposition(HttpContext context, string fileName)
        {
            ContentDisposition contentDisposition = new ContentDisposition
            {
                FileName = fileName,
                Inline = false  // false = prompt the user for downloading;  true = browser to try to show the file inline
            };
            context.Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
        }


        private static async Task GetResponseBody(HttpContext context, FileStream file)
        {
            Stream originalBody = context.Response.Body;
            try
            {
                context.Response.Body = originalBody;
                file.Seek(0, SeekOrigin.Begin);
                await file.CopyToAsync(context.Response.Body);
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }

    }
}
