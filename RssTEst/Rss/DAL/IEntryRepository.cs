using System.Collections.Generic;
using Rss.DTO;

namespace Rss.DAL
{
    public interface IEntryRepository
    {
        void InsertOne(FeedEntry entry);

        List<FeedEntry> GetBySource(FeedSource source);

    }
}
