using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

    }
}
