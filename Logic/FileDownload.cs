using System.IO;

namespace FileDownloadApp.Logic
{
    public class FileDownload
    {
        public FileStream GetFile(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

            if (File.Exists(path))
            {
                return (new FileStream(path, FileMode.Open, FileAccess.Read));
            }
            else
            {
                return null;
            }
        }
    }
}
