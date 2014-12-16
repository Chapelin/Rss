using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss.Services
{
    interface IFeedProcessor
    {
        /// <summary>
        /// Processes the feed
        ///     1 - Get FeedsData
        ///     2 - Store in DB.
        /// </summary>
        /// <param name="url">The URLto parse</param>
        void ProcessFeed(FeedSource source);
    }
}
