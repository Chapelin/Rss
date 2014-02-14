using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using RssApi.Helper;
using RssEntity;

namespace RssApi.Controllers
{
    public class EntreeController : ApiController
    {
        public IEnumerable<Entree> GetLast()
        {
            var col = DBManager.Entrees.FindAll().SetSortOrder(SortBy.Descending("Date")).Take(ConfigHelper.NumberLast);
            return col;
        }

        public IEnumerable<Entree> GetBySourceID(string id)
        {
            return DBManager.Entrees.Find(Query<Entree>.EQ(x => x.SourceId, id)).SetSortOrder(SortBy.Descending("Date"));
        }

        public IEnumerable<Entree> GetLastBySourceID(string id)
        {
            var res = DBManager.Entrees.Find(Query<Entree>.EQ(x => x.SourceId, id)).SetSortOrder(SortBy.Descending("Date")).Take(ConfigHelper.NumberLast);
            return res;
        }

        private IEnumerable<Entree> GetLastBySourcesID(List<string> ids)
        {
            var res = DBManager.Entrees.Find(Query<Entree>.Where(x => ids.Contains(x.SourceId))).SetSortOrder(SortBy.Descending("Date")).Take(ConfigHelper.NumberLast);
            return res;
        }

        public IEnumerable<Entree> GetLastByCategorie(string id)
        {
            var listeSources = DBManager.Sources.Find(Query<Source>.Where(x => x.CategoriesIds.Contains(id))).Select(x=> x.Id).ToList();
            return GetLastBySourcesID(listeSources);

        }
    }
}
