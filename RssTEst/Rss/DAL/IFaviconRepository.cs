using Rss.DTO;

namespace Rss.DAL
{
    public interface IFaviconRepository
    {
        void InsertOne(Favicon fv);

        Favicon GetFromFeedSource(FeedSource source);
    }
}
