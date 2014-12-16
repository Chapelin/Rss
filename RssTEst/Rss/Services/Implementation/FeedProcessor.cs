using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss.Services.Implementation
{
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;

    using MongoDB.Driver.Builders;

    using Rss.DAL;
    using Rss.DTO;

    public class FeedProcessor : IFeedProcessor
    {
        private readonly IFeedGetter getter;

        private readonly IRssRepository rssRepository;

        public FeedProcessor(IFeedGetter getter,IRssRepository rssRepository)
        {
            this.getter = getter;
            this.rssRepository = rssRepository;
        }

        public void ProcessFeed(FeedSource source)
        {
            var feedsData = getter.GetFeedsData(source);
            this.SortFeeds(feedsData, source);

        }

        public async void SortFeeds(FeedsData feedsData,FeedSource source)
        {
            var lastDl = this.rssRepository.Entries.Find(Query<FeedEntry>.EQ(x => x.Source, source))
                .Sort(SortBy<FeedEntry>.Descending(x => x.CreatedOn))
                .Limit(1);
            var limitDate = DateTime.MinValue;
            if (await lastDl.CountAsync() > 0)
            {
                limitDate = (await lastDl.ToCursorAsync(new CancellationToken())).Current.First().CreatedOn;
            }

            //here limit date contains the datetime to use as filter

            throw new NotImplementedException();
        }
    }
}
