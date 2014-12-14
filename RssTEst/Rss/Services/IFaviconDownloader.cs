using Rss.DTO;

namespace Rss.Services
{
    public interface IFaviconDownloader
    {
        /// <summary>
        /// Gets the favicon from an URL.
        /// </summary>
        /// <param name="url">The URL</param>
        /// <returns>The Favicon</returns>
        Favicon GetFaviconFromUrl(string url);
    }
}
