using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Rss.DTO;

namespace Rss.DAL
{
    interface IFeedEntryRepository
    {
        int AddFeedEntry(FeedEntry feedEntryToAdd);

        bool DeleteFeedEntry(int feedEntryId);

        List<FeedEntry> GetFeedSourcesFiltered(Func<FeedEntry, bool> filter);
    }
}
