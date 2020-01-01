using System.Collections.Generic;

namespace FileDownloadApp.Common
{
    public static class MimeTypes
    {
        public static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".png", "image/png"}
            };
        }

    }
}
