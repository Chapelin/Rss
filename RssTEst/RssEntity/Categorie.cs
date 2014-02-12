
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RssEntity
{
    public class Categorie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; }

    }
}
