using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using RssEntity;

namespace RssApi.Controllers
{
    public class EntreeController : ApiController
    {
        public IEnumerable<Entree> Get()
        {
            var col = DBManager.Entrees.FindAll();
            return col;
        }

        public IEnumerable<Entree> GetBySourceID(string id)
        {
            return DBManager.Entrees.Find(Query<Entree>.EQ(x => x.SourceId, id));
        }

        public IEnumerable<Entree> GetLast20BySourceID(string id)
        {
            var res =  DBManager.Entrees.Find(Query<Entree>.EQ(x => x.SourceId, id)).Take(20);
            return res;
        }
    }
}
