using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RssEntity;
using ServiceRssToDB;

namespace RssApi.Controllers
{
    public class SourceController : ApiController
    {
        public IEnumerable<Source> Get()
        {
            return DBManager.Rss.GetCollection<Source>("Sources").FindAll();
        }

        
    }
}
