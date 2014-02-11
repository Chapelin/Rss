using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using NLog;
using RssEntity;

namespace ServiceRssToDB
{
    public class DBManager
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private static MongoDatabase _rss;
        private static MongoClient _client;
        private static MongoDatabase Rss
        {
            get
            {
                if (_rss == null)
                {
                    if (_client == null)
                    {
                        _client = new MongoClient("mongodb://localhost");
                    }
                    _rss = _client.GetServer().GetDatabase("test");
                }
                return _rss;
            }
        }

        public static MongoCollection<Entree> Entrees
        {
            get { return Rss.GetCollection<Entree>("Entrees"); }
        }

        public static MongoCollection<Source> Sources
        {
            get { return Rss.GetCollection<Source>("Sources"); }
        }

        public static MongoCollection<Categorie> Categories
        {
            get { return Rss.GetCollection<Categorie>("Categories"); }
        }
        
    }
}
