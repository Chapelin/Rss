namespace Rss.Services.Implementation
{
    using System;
    using System.ServiceModel.Syndication;
    using System.Threading.Tasks;
    using System.Xml;

    using Rss.DTO;

    public class FeedGetter : IFeedGetter
    {

        private readonly INetDownloader downloader;

        private readonly IFeedFormatterFactory formatterFactory;

        public FeedGetter(INetDownloader downloader, IFeedFormatterFactory formatterFactory)
        {
            this.downloader = downloader;
            this.formatterFactory = formatterFactory;
        }

        public async Task<FeedsData> GetFeedsData(FeedSource source)
        {
            var result = new FeedsData(source);
            result.LaunchTime = DateTime.Now.ToString("s");
            result.UrlFeed = source.FeedUrl;

            SyndicationFeed resultFeed = null;
            var streamFeed = this.downloader.GetStreamFromUrl(source.FeedUrl);
            var xmlReader = XmlReader.Create(streamFeed);
            var feedFormatter = this.formatterFactory.GetFeedFormatter(source.FormatterName);

            resultFeed = this.GetFeed(xmlReader, feedFormatter);
            result.FeedData = resultFeed;
            result.FinishedTime = DateTime.Now.ToString("s");
            return result;
        }

        public SyndicationFeed GetFeed(XmlReader xmlReader, IFeedFormatter feedFormatter)
        {
            if (!feedFormatter.CanRead(xmlReader))
            {
                throw new Exception("Can read");
            }

            feedFormatter.ReadFrom(xmlReader);
            return feedFormatter.Feed;
        }
    }
}
