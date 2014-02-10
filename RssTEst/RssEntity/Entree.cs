using System;
using MongoDB.Bson;

namespace RssEntity
{
    public class Entree
    {
        public ObjectId Id { get; set; }
        public string Texte { get; set; }
        public string Titre { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateInsertion { get; set; }
        public  ObjectId SourceId { get; set; }
    }
}
