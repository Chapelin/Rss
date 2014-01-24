using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
        private Type FormatterType;
        public override string ToString()
        {
            return String.Format(" Id : {0} - URL : {1} : ", this.RssFluxId,
                this.URL);
        }

        public RssScrapper(string url, int id, double delay, string formatterType = null)
        {

            this.URL = url;
            this.RssFluxId = id;
            this.Delay_seconds = delay;
            if (!string.IsNullOrWhiteSpace(formatterType))
                FormatterType = Assembly.Load("Readers").CreateInstance("Readers." + formatterType).GetType();


            Logger.Log(this + "initialized");

        }


        public void Launch()
        {
            Logger.Log(this + "Launched");
            DateTime next;
            while (true)
            {
                next = DateTime.Now.AddMilliseconds(Delay_seconds * 1000);
                ScrapRss();
                var toSleep = next - DateTime.Now;
                if (toSleep.TotalMilliseconds > 0)
                    Thread.Sleep(toSleep);
            }

        }

        public void ScrapRss()
        {

            _client = new WebClient();
            _client.Proxy = WebRequest.DefaultWebProxy;
            _client.Proxy.Credentials = CredentialCache.DefaultCredentials;
            _client.Credentials = CredentialCache.DefaultCredentials;
            SyndicationFeed feed = null;

            try
            {
                // Read the feed using an XmlReader  
                using (XmlReader reader = XmlReader.Create(OpenUrl(4)))
                {
                    Logger.LogFormat(this + "url OK", this.URL);
                    var liste = new List<Flux>();
                    //formatter custom
                    if (FormatterType != null)
                    {
                        var constr = FormatterType.GetConstructor(new Type[] { });
                        SyndicationFeedFormatter tempo = (SyndicationFeedFormatter)constr.Invoke(new object[] { });
                        if (!tempo.CanRead(reader))
                            throw new Exception(this + " le formatter n'est pas bon : " + tempo.GetType());
                        tempo.ReadFrom(reader);
                        feed = tempo.Feed;
                    }
                    else //formatter classique
                        feed = SyndicationFeed.Load(reader);
                    foreach (var elem in feed.Items)
                    {
                        try
                        {
                            var temp = new Flux()
                            {
                                date = elem.PublishDate.DateTime,
                                ID = RssFluxId,

                                title = elem.Title == null ? string.Empty : elem.Title.Text,
                                text = elem.Summary == null ? string.Empty : elem.Summary.Text,
                                dateInsert = DateTime.Now,
                                link = "",
                                image = ""


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
                        catch (Exception e)
                        {
                            Logger.Log(this + "Erreur sur un element");
                            throw;
                        }

                    }
                    Logger.LogFormat(this + "Info recuperées, en attente mise en base", this.URL);
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

                    }
                    Logger.LogFormat(this + "nombre d'entrée à inserer {0}", liste.Count);
                    foreach (var flux in liste)
                    {
                        DBManager.Manager.Insert(flux.ToRequest());
                    }

                    Logger.Log(this + "Scrap ok ");
                }
            }
            catch (Exception e)
            {
                Logger.LogFormat(this + "Exception RssScrapper : {0}", e.GetType());
            }

        }



        public Stream OpenUrl(int trynumber)
        {
            Stream result = null;
            for (int i = 0; i < trynumber; i++)
            {
                try
                {
                    result = _client.OpenRead(URL);
                    break;
                }
                catch (WebException)
                {
                }
            }
            if (result == null)
            {
                throw new Exception(string.Format("plus de {0} erreurs lors du telechargement de {1}", trynumber, URL));
            }
            return result;
        }
    }
}
