using MongoDB.Driver;
using Rss.DTO;

namespace Rss.Services
{
    interface IDatabaseConnector
    {
        bool Connect();

        MongoCollection<FeedEntry> FeedEntries { get;  }

        MongoCollection<FeedSource> FeedSources { get; } 

    }
}
