using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RssEntity
{
    public class Entree
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Texte { get; set; }
        public string Titre { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Date { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateInsertion { get; set; }
         [BsonRepresentation(BsonType.ObjectId)]
        public  string SourceId { get; set; }
    }
}
