using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rss.DTO;

namespace Rss.Services.Implementation
{
    public class FaviconDownloader : IFaviconDownloader
    {

        private INetDownloader netdowloader;

        public FaviconDownloader(INetDownloader netdowloader)
        {
            this.netdowloader = netdowloader;
        }

        public Favicon GetFaviconFromFeedUrl(string url)
        {
            List<Uri> listPotentialUris = this.GetPotentialUriForFavicon(url);
        }

        public List<Uri> GetPotentialUriForFavicon(string url)
        {
            throw new NotImplementedException();
        }
    }
}
