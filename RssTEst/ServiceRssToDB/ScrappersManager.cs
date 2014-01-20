using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRssToDB
{
    public class ScrappersManager
    {
        private List<RssScrapper> liste_scrapper;


        public ScrappersManager()
        {
            Logger.Log("ScrapperManager : constructor");
            liste_scrapper = new List<RssScrapper>();
        }



        public void  InitListeScrap()
        {
            Logger.Log("ScrapperManager : InitListeScrap begin");
            var requete = "select IdFlux, base_url, delay from ListeFlux;";

            var res = DBManager.Manager.Select(requete);

            foreach (DataRow row in res.Rows)
            {
                var id = Convert.ToInt32(row["IdFlux"]);
                var url = Convert.ToString(row["base_url"]);
                var delay = Convert.ToDouble(row["delay"]);

                RssScrapper temp = new RssScrapper(url,id,delay);
                liste_scrapper.Add(temp);

            }
            Logger.LogFormat("ScrapperManager : {0} Scrappers",liste_scrapper.Count);
        }

        public void Scrap()
        {
            Logger.Log("ScrapManager : beginscrap");
            List<Task> listTask = new List<Task>();
            foreach (var rssScrapper in liste_scrapper)
            {
                var tempo = rssScrapper;
                listTask.Add(Task.Factory.StartNew(tempo.Launch));
            }
            Logger.Log("ScrapManager : All scrapper launched");
            Task.WaitAll(listTask.ToArray());
            Logger.Log("ScrapMaanger : Scrap ended");
        }


    }
}
