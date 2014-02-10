using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver.Builders;
using NLog;
using RssEntity;

namespace ServiceRssToDB
{
    public class ScrappersManager
    {

        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private List<RssScrapper> liste_scrapper;

        
        public ScrappersManager()
        {
            logger.Info("Constructor");
            liste_scrapper = new List<RssScrapper>();
        }



        public void  InitListeScrap()
        {
            logger.Info("InitListeScrap begin");

            var coll = DBManager.Rss.GetCollection<Source>("Sources");
            foreach (var row in coll.FindAll())
            {
           

                var temp = new RssScrapper(row.URL,row,row.Delai,row.Formatter);
                liste_scrapper.Add(temp);

            }
            logger.Info("{0} Scrappers", liste_scrapper.Count);
        }

        public void Scrap()
        {
            logger.Info("Beginscrap");
            var listTask = new List<Task>();
            foreach (var rssScrapper in liste_scrapper)
            {
                Thread.Sleep(50);
                var tempo = rssScrapper;
                listTask.Add(Task.Factory.StartNew(tempo.Launch));
            }
            logger.Info("All scrapper launched");
            Task.WaitAll(listTask.ToArray());
            logger.Info("Scrap ended");
        }


    }
}
