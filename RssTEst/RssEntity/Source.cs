using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;

namespace RssEntity
{
    public class Source
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public string Formatter { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateAjout { get; set; }
        public int Delai { get; set; }
        public List<string> CategoriesIds { get; set; }

        [BsonIgnore]
        public List<Categorie> Categories {
            get
            {
                return DBManager.Categories.Find(Query<Categorie>.Where(x => CategoriesIds.Contains(x.Id))).ToList();
            }
       }
 

    }
}
