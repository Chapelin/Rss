using System;
using Rss.DTO;

namespace Rss.DAL.Implementation
{
    public class FaviconRepository : IFaviconRepository
    {
        private IRssRepository rssRepository;

        public FaviconRepository(IRssRepository rssRepository)
        {
            this.rssRepository = rssRepository;
        }

        public void InsertOne(Favicon fv)
        {
            throw new NotImplementedException();
        }
    }
}
