using System;
using MongoDB.Bson;

namespace RssEntity
{
    public class Source
    {
        public ObjectId Id { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public string Formatter { get; set; }
        public DateTime DateAjout { get; set; }
        public int Delai { get; set; }

    }
}
