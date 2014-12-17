namespace Rss.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Threading;
    using System.Threading.Tasks;

    using MongoDB.Driver.Builders;

    using Rss.DAL;
    using Rss.DTO;

    public class FeedProcessor : IFeedProcessor
    {
        private readonly IFeedGetter getter;

        private readonly IRssRepository rssRepository;

        public FeedProcessor(IFeedGetter getter, IRssRepository rssRepository)
        {
            this.getter = getter;
            this.rssRepository = rssRepository;
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
            var feedsData = this.getter.GetFeedsData(source);
            var listFeed = this.MapFeedToFeedEntry(feedsData, source);
            this.SaveFeeds(await listFeed);
        }

        /// <summary>
        /// Save a list of feed entries into DB
        /// </summary>
        /// <param name="listFeed">
        /// The list of entries.
        /// </param>
        private void SaveFeeds(IEnumerable<FeedEntry> listFeed)
        {
            Task.WaitAll(listFeed.Select(feedEntry => this.rssRepository.Entries.InsertOneAsync(feedEntry)).ToArray());
        }

        /// <summary>
        /// Sort the feeds items based on the last updated of the source.
        /// </summary>
        /// <param name="feedsData">
        /// The feeds data.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IEnumerable<SyndicationItem>> SortFeeds(FeedsData feedsData, FeedSource source)
        {
            var lastDl = this.rssRepository.Entries.Find(Query<FeedEntry>.EQ(x => x.Source, source))
                .Sort(SortBy<FeedEntry>.Descending(x => x.CreatedOn))
                .Limit(1);
            var limitDate = DateTime.MinValue;
            if (await lastDl.CountAsync() > 0)
            {
                limitDate = (await lastDl.ToCursorAsync(new CancellationToken())).Current.First().CreatedOn;
            }

            var listToAdd =
                feedsData.FeedData.Items.Where(
                    x => (x.LastUpdatedTime > limitDate));



            return listToAdd;

        }

        /// <summary>
        /// Map the feed items to a list of feed entries, sorted
        /// </summary>
        /// <param name="feedsData">
        /// The feeds data.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<List<FeedEntry>> MapFeedToFeedEntry(FeedsData feedsData, FeedSource source)
        {
            var result = new List<FeedEntry>();
            var items = await this.SortFeeds(feedsData, source);
            foreach (var syndicFeedItem in items)
            {
                var feedEntry = new FeedEntry();
                feedEntry.CreatedOn = syndicFeedItem.PublishDate.DateTime;
                feedEntry.AddedOn = DateTime.Now;
                feedEntry.Content = syndicFeedItem.Content.ToString(); //TODO : helper content to string
                feedEntry.Source = source;
                feedEntry.Title = syndicFeedItem.Title.Text;
                feedEntry.UpdatedOn = null;
                feedEntry.Version = 1; // TODO : version helper
                result.Add(feedEntry);
            }


            return result;
        }
    }
}
