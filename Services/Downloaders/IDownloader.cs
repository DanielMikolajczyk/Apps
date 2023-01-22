using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Services.Downloaders
{
    interface IDownloader
    {
        public int download(int id);
        public void downloadAll(string year);
    }
}
