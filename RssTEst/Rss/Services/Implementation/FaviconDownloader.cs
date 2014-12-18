using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Rss.DAL;
using Rss.DTO;

namespace Rss.Services.Implementation
{
    public class FaviconDownloader : IFaviconDownloader
    {

        private INetDownloader netdowloader;
        private readonly IRssRepository _rssRepository;

        public FaviconDownloader(INetDownloader netdowloader, IRssRepository rssRepository)
        {
            this.netdowloader = netdowloader;
            _rssRepository = rssRepository;
        }

        public Favicon GetFaviconFromFeedSource(FeedSource source)
        {
            var listUri = new List<Uri>();
            listUri.Add(this.CalculateFaviconPotentialPosition(source.SiteUrl));
            listUri.Add(this.FindFaviconInHead(source.SiteUrl));
            var stream = this.GetAFavicon(listUri);
            
            //TODO : dl & define type
            var fav = this.SaveToDB(stream, source);
            return fav;
        }

        private Stream GetAFavicon(List<Uri> listUri)
        {
            //TODO : foreach : if not 400/etc => get stream & stop
            throw new NotImplementedException();
        }

        private Uri FindFaviconInHead(string url)
        {
            Uri result = null;
            var t = new HtmlDocument();
            t.Load(this.netdowloader.GetStreamFromUrl(url));
            var head = t.DocumentNode.Descendants("link")
                .Where(x=> x.Attributes["rel"] != null)
                    .Where(x=> x.Attributes["rel"].Value == "icon" || x.Attributes["rel"].Value == "shortcut icon");
            if (head != null && head.Any())
            {
                UriBuilder ub = new UriBuilder();
                result = new Uri(new Uri(ub.Host), head.First().GetAttributeValue("href", "favicon.ico"));
            }
            return result;
        }

        public Uri CalculateFaviconPotentialPosition(string url)
        {

            throw new NotImplementedException();
        }

        public Favicon SaveToDB(Stream input, FeedSource source)
        {
            var gridFsInfo = this._rssRepository.GridFS.Upload(input, source.Name);
            var fileId = gridFsInfo.Id;
            var fv = new Favicon {GridFSId = fileId.AsObjectId.ToString(),SourceAssociated = source};
            this._rssRepository.Favicons.InsertOneAsync(fv);
            return fv;
        }
    }
}
