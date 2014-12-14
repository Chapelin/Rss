using System.ServiceModel.Syndication;
using System.Xml;

namespace Rss.Services
{
    interface IFeedFormatter
    {
        /// <summary>
        /// Determines whether this instance can read the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        bool CanRead(XmlReader reader);

        /// <summary>
        /// Gets the RDF namespace URI.
        /// </summary>
        /// <value>
        /// The RDF namespace URI.
        /// </value>
        string RdfNamespaceUri { get; }

        /// <summary>
        /// Gets the namespace URI.
        /// </summary>
        /// <value>
        /// The namespace URI.
        /// </value>
        string NamespaceUri { get; }

        /// <summary>
        /// Reads the Feed from a reader
        /// </summary>
        /// <param name="reader">The reader.</param>
        void ReadFrom(XmlReader reader);

        /// <summary>
        /// Gets the feed.
        /// </summary>
        /// <value>
        /// The feed.
        /// </value>
        SyndicationFeed Feed { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        string Version { get; set; }
    }
}
