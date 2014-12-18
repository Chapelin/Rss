using MongoDB.Driver.GridFS;

namespace Rss.DAL
{
    using MongoDB.Driver;

    using Rss.DTO;

    /// <summary>
    /// The RssRepository interface.
    /// </summary>
    public interface IRssRepository
    {
        /// <summary>
        /// Gets the entries.
        /// </summary>
        IMongoCollection<FeedEntry> Entries { get; }

        /// <summary>
        /// Gets the sources.
        /// </summary>
        IMongoCollection<FeedSource> Sources { get; }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        IMongoCollection<SourceCategory> Categories { get; }

        /// <summary>
        /// Gets the favicons.
        /// </summary>
        IMongoCollection<Favicon> Favicons { get; }

       MongoGridFS GridFS { get; }
        


    }
}
