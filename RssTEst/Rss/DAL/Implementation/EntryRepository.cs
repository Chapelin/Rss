using System;
using System.Collections.Generic;
using Rss.DTO;

namespace Rss.DAL.Implementation
{
    public class EntryRepository : IEntryRepository
    {
        private IRssRepository _rssRepository;

        public EntryRepository(IRssRepository rssRepository)
        {
            _rssRepository = rssRepository;
        }

        public void InsertOne(FeedEntry entry)
        {
            throw new NotImplementedException();
        }

        public List<FeedEntry> GetBySource(FeedSource source)
        {
            throw new NotImplementedException();
        }


    }
}
