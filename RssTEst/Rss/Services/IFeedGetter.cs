using Rss.DTO;

namespace Rss.Services
{
    public interface IFeedGetter
    {
        /// <summary>
        /// Gets the feeds data.
        /// </summary>
        /// <param name="url">The URL to process</param>
        /// <param name="feedFormatter"></param>
        /// <returns>The FeedsData</returns>
        FeedsData GetFeedsData(FeedSource source);
    }
}
