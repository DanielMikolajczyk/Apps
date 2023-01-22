using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Services.Downloaders
{
    public abstract class Downloader : IDownloader
    {
        protected string _path = @"C:\Users\dmiko\Desktop\inzynierka\Apps\Resources\";
        protected string _directorySeparator = @"\";
        protected string _fileExtension = @".pdf";
        public Downloader()
        {

        }

        public virtual int download(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void downloadAll(string year)
        {
            throw new NotImplementedException();
        }

        protected void createDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
