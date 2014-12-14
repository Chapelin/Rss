using System.IO;

namespace Rss.Services
{
    public interface INetDownloader
    {
        /// <summary>
        /// Gets the stream from an URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The stream of the content</returns>
        Stream GetStreamFromUrl(string url);
    }
}
