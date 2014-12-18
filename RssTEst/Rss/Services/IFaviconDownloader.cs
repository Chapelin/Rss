using Rss.DTO;

namespace Rss.Services
{
    public interface IFaviconDownloader
    {
        /// <summary>
        /// Gets the favicon from an URL.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>The Favicon</returns>
        Favicon GetFaviconFromFeedSource(FeedSource source);
    }
}
