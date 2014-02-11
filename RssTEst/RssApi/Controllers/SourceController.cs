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

namespace RssApi.Controllers
{
    public class SourceController : ApiController
    {
        public IEnumerable<Source> Get()
        {
            return DBManager.Sources.FindAll();
        }

        public Source GetById(string id)
        {
            var uid = new ObjectId(id);
            return DBManager.Sources.Find(Query<Source>.EQ(x => x.Id, uid)).FirstOrDefault();
        }

        public IEnumerable<Categorie> GetCategoriesOf(string id)
        {
            var uid = new ObjectId(id);
            var source = DBManager.Sources.Find(Query<Source>.EQ(x => x.Id, uid)).FirstOrDefault();
            return
                DBManager.Categories.Find(Query<Categorie>.In(x => x.Id,
                                                                                              source.CategoriesIds));
        }

        
    }
}
