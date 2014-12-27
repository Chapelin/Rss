namespace Rss.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Threading;
    using System.Threading.Tasks;

    using MongoDB.Driver.Builders;

    using DAL;
    using Rss.DTO;

    public class FeedProcessor : IFeedProcessor
    {
        private readonly IFeedGetter getter;

        private IFaviconRepository faviconRepository;

        private IEntryRepository entryRepository;

        //private IFaviconDownloader favDownloader;

        public FeedProcessor(IFeedGetter getter, IFaviconRepository faviconRepository, IEntryRepository entryRepository)
        {
            this.getter = getter;
            this.faviconRepository = faviconRepository;
            this.entryRepository = entryRepository;
            //this.favDownloader = favDownloader;
        }

        /// <summary>
        /// Get, sort and save feeds from a source
        /// </summary>
        /// <param name="source">
        /// The source of the feeds.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ProcessFeed(FeedSource source)
        {
            var t = this.HandleFavicon(source);
            var feedsData = this.getter.GetFeedsData(source);
            var listFeed = this.MapFeedToFeedEntry(await feedsData);
            this.SaveFeeds(await listFeed);
            await t;
        }


        public async Task HandleFavicon(FeedSource source)
        {
            if (source.Favicon == null)
            {
                source.Favicon = this.faviconRepository.GetFromFeedSource(source);
            }
        }

        /// <summary>
        /// Save a list of feed entries into DB
        /// </summary>
        /// <param name="listFeed">
        /// The list of entries.
        /// </param>
        private void SaveFeeds(IEnumerable<FeedEntry> listFeed)
        {
            foreach (var feedEntry in listFeed)
            {
                this.entryRepository.InsertOne(feedEntry);
            }
        }

        /// <summary>
        /// Sort the feeds items based on the last updated of the source.
        /// </summary>
        /// <param name="feedsData">
        ///     The feeds data.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IEnumerable<SyndicationItem>> SortFeeds(FeedsData feedsData)
        {
            var lastDL = this.entryRepository.GetBySource(feedsData.Source).Max(x => x.CreatedOn);
           
            var listToAdd =
                feedsData.FeedData.Items.Where(
                    x => (x.LastUpdatedTime > lastDL));

            return listToAdd;
        }

        /// <summary>
        /// Map the feed items to a list of feed entries, sorted
        /// </summary>
        /// <param name="feedsData">
        ///     The feeds data.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<List<FeedEntry>> MapFeedToFeedEntry(FeedsData feedsData)
        {
            var result = new List<FeedEntry>();
            var items = await this.SortFeeds(feedsData);
            foreach (var syndicFeedItem in items)
            {
                var feedEntry = new FeedEntry();
                feedEntry.CreatedOn = syndicFeedItem.PublishDate.DateTime;
                feedEntry.AddedOn = DateTime.Now;
                feedEntry.Content = syndicFeedItem.Content.ToString(); //TODO : helper content to string
                feedEntry.Source = feedsData.Source;
                feedEntry.Title = syndicFeedItem.Title.Text;
                feedEntry.UpdatedOn = null;
                feedEntry.Version = 1; // TODO : version helper
                result.Add(feedEntry);
            }


            return result;
        }
    }
}
