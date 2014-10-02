using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using Rss.DTO;

namespace Rss.Services
{
    public interface IFeedProcessor
    {
        SyndicationFeedFormatter GetFeedFormatter(FeedSource feedSource);

        SyndicationFeed GetFeeds(Stream data, SyndicationFeedFormatter formatter);

        List<FeedEntry> ProcessFeeds(SyndicationFeed feeds);

        List<FeedEntry> FilterFeeds(List<FeedEntry> listToFilter);

        int InsertFeeds(List<FeedEntry> listToInsert);
    }
}
