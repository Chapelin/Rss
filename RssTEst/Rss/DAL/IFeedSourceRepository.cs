using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Rss.DTO;

namespace Rss.DAL
{
    interface IFeedSourceRepository
    {
        List<FeedSource> GetAllFeedSources();

        List<FeedSource> GetFeedSourcesFiltred(Func<FeedSource,bool> filter);

        int AddFeedSource(FeedSource sourceToAdd);

        bool DeleteFeedSource(int sourceId);
    }
}
