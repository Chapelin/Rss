using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss.Services
{
    interface IDownloader
    {
        Stream GetDataStream(string url);
    }
}
