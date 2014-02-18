using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RssEntity
{
    public class Favicon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string SourceId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string GridFSId { get; set; }

    }
}
