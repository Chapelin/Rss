using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Rss.DTO;

namespace Rss.Services.Implementation
{
    public class FeedGetter : IFeedGetter
    {
        public FeedsData GetFeedsData(string url, IFeedFormatter feedFormatter)
        {
            var result = new FeedsData();
            result.LaunchTime = DateTime.Now.ToString("s");
            result.UrlFeed = url;

            SyndicationFeed resultFeed = null;
            var favDownloader = new FaviconDownloader();
            var favicon = favDownloader.GetFaviconFromFeedUrl(url);
            var downloader = new FeedDownloader();
            var streamFeed = downloader.GetStreamFromUrl(url);
            var xmlReader = XmlReader.Create(streamFeed);
            if (feedFormatter.CanRead(xmlReader))
            {
                resultFeed = this.GetFeed(xmlReader, feedFormatter);
            }

            result.FeedData = resultFeed;
            result.Favicon = favicon;
            result.FinishedTime = DateTime.Now.ToString("s");
            return result;
        }

        public SyndicationFeed GetFeed(XmlReader xmlReader, IFeedFormatter feedFormatter)
        {
            throw new NotImplementedException();
        }
    }
}
