using Rss.DTO;

namespace Rss.DAL
{
    public interface IEntryRepository
    {
        void InsertOne(FeedEntry entry);

        void GetBySource(FeedSource source);

    }
}
