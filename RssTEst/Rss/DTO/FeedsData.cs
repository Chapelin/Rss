namespace Rss.DTO
{
    using System.ServiceModel.Syndication;

    public class FeedsData
    {
        public FeedsData(FeedSource source)
        {
            this.Source = source;
        }

        /// <summary>
        /// Gets or sets the feed's url.
        /// </summary>
        /// <value>
        /// The url.
        /// </value>
        public string UrlFeed { get; set; }

        /// <summary>
        /// Gets or sets the launch time.
        /// </summary>
        /// <value>
        /// The launch time.
        /// </value>
        public string LaunchTime { get; set; }

        /// <summary>
        /// Gets or sets the finished time.
        /// </summary>
        /// <value>
        /// The finished time.
        /// </value>
        public string FinishedTime { get; set; }

        /// <summary>
        /// Gets or sets the feed data.
        /// </summary>
        /// <value>
        /// The feed data.
        /// </value>
        public SyndicationFeed FeedData { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        public FeedSource Source { get; set; }
    }
}
