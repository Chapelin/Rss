using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using RssEntity;
using ServiceRssToDB;

namespace RssApi.App_Start
{
    public class EntreeController : ApiController
    {
        public IEnumerable<Entree> Get()
        {
            var col = DBManager.Rss.GetCollection<Entree>("Entree").FindAll();
            return col;
        }

        public IEnumerable<Entree> Get(string id)
        {
            var uid = new ObjectId(id);
            return DBManager.Rss.GetCollection<Entree>("Entree").Find(Query<Entree>.EQ(x => x.SourceId, uid));
        }
    }
}
