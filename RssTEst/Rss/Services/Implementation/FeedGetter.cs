namespace Rss.Services.Implementation
{
    using System;
    using System.ServiceModel.Syndication;
    using System.Xml;

    using Rss.DTO;

    public class FeedGetter : IFeedGetter
    {
        private readonly IFaviconDownloader favDownloader;

        private readonly INetDownloader downloader;

        private readonly IFeedFormatterFactory formatterFactory;

        public FeedGetter(IFaviconDownloader favDownloader, INetDownloader downloader, IFeedFormatterFactory formatterFactory)
        {
            this.favDownloader = favDownloader;
            this.downloader = downloader;
            this.formatterFactory = formatterFactory;
        }

        public FeedsData GetFeedsData(FeedSource source)
        {
            var result = new FeedsData();
            result.LaunchTime = DateTime.Now.ToString("s");
            result.UrlFeed = source.FeedUrl;

            SyndicationFeed resultFeed = null;
            var favicon = favDownloader.GetFaviconFromFeedUrl(source.SiteUrl);
            var streamFeed = downloader.GetStreamFromUrl(source.FeedUrl);
            var xmlReader = XmlReader.Create(streamFeed);
            var feedFormatter = formatterFactory.GetFeedFormatter(source.FormatterName);
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
