using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading;
using System.Xml;

namespace ServiceRssToDB
{
    public class RssScrapper
    {
        public string URL { get; set; }
        public int RssFluxId { get; set; }
        private WebClient _client;
        private double Delay_seconds;


        public RssScrapper(string url, int id, double delay)
        {
            this.URL = url;
            this.RssFluxId = id;
            _client = new WebClient();
            _client.Proxy = WebRequest.DefaultWebProxy;
            _client.Proxy.Credentials = CredentialCache.DefaultCredentials;
            _client.Credentials = CredentialCache.DefaultCredentials;
            this.Delay_seconds = delay;
            Logger.Log("RssScrapper initialized");

        }


        public void Launch()
        {
            Logger.Log("Launched");
            DateTime next;
            while (true)
            {
                next = DateTime.Now.AddMilliseconds(Delay_seconds*1000);
                ScrapRss();
                var toSleep = next - DateTime.Now;
                Thread.Sleep(toSleep);
            }
        }

        public void ScrapRss()
        {
            try
            {
                // Read the feed using an XmlReader  
                using (XmlReader reader = XmlReader.Create(_client.OpenRead(URL)))
                {
                    Logger.LogFormat("RssScrapper : url OK {0}",this.URL);
                    var liste = new List<Flux>();
                    var feed = SyndicationFeed.Load(reader);
                    foreach (var elem in feed.Items)
                    {
                        var temp = new Flux()
                        {
                            date = elem.PublishDate.DateTime,
                            ID = RssFluxId,

                            title = elem.Title.Text,
                            text = elem.Summary.Text,
                            dateInsert = DateTime.Now

                        };
                        if (elem.Links.Count > 0)
                        {
                            temp.link = elem.Links[0].GetAbsoluteUri().ToString();
                        }
                        if (elem.Links.Count > 1)
                        {
                            temp.image = elem.Links[1].GetAbsoluteUri().ToString();
                        }
                        liste.Add(temp);
                    }

                    liste = liste.OrderBy(x => x.date).ToList();
                    var requete =
                        string.Format(
                            "select date from Flux inner join ListeFlux on Flux.IdFlux = ListeFlux.IdFlux  where ListeFlux.Base_Url = '{0}' order by date desc LIMIT  1;",
                            URL);

                    var last = DBManager.Manager.Select(requete);
                    if (last.Rows.Count > 0)
                    {
                        
                        var lastDate = DateTime.Parse(Convert.ToString(last.Rows[0]["date"]));
                        liste = liste.Where(x => x.date > lastDate).ToList();
                        Logger.LogFormat("RssScrapper : nombre d'entrée à inserer dans {0} : {1}",this.URL,liste.Count);
                    }
                    foreach (var flux in liste)
                    {
                        DBManager.Manager.Insert(flux.ToRequest());
                    }

                    Logger.LogFormat("Scrap ok : {0}", this.URL);
                }
            }
            catch (Exception e)
            {
                Logger.Log(string.Format("Exception RssScrapper : {0}, url : {1}",e.GetType(),this.URL));                
            }
          
        }
    }
}
