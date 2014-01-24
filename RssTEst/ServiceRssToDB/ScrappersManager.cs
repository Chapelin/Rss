using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRssToDB
{
    public class ScrappersManager
    {
        private List<RssScrapper> liste_scrapper;


        public override string ToString()
        {
            return "ScrappersManager : ";
        }

        public ScrappersManager()
        {
            Logger.Log("constructor");
            liste_scrapper = new List<RssScrapper>();
        }



        public void  InitListeScrap()
        {
            Logger.Log(this+"InitListeScrap begin");
            var requete = "select IdFlux, base_url, delay, Format from ListeFlux;";

            var res = DBManager.Manager.Select(requete);

            foreach (DataRow row in res.Rows)
            {
                var id = Convert.ToInt32(row["IdFlux"]);
                var url = Convert.ToString(row["base_url"]);
                var delay = Convert.ToDouble(row["delay"]);
                var format = Convert.ToString(row["Format"]);

                RssScrapper temp = new RssScrapper(url,id,delay,format);
                liste_scrapper.Add(temp);

            }
            Logger.LogFormat(this + "{0} Scrappers", liste_scrapper.Count);
        }

        public void Scrap()
        {
            Logger.Log(this + "beginscrap");
            List<Task> listTask = new List<Task>();
            foreach (var rssScrapper in liste_scrapper)
            {
                Thread.Sleep(50);
                var tempo = rssScrapper;
                listTask.Add(Task.Factory.StartNew(tempo.Launch));
            }
            Logger.Log(this + "All scrapper launched");
            Task.WaitAll(listTask.ToArray());
            Logger.Log(this + "Scrap ended");
        }


    }
}
