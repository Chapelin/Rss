using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace ServiceRssToDB
{
    public class RssScrapper
    {
        public string URL { get; set; }
        public int RssFluxId { get; set; }
        private WebClient _client;



        public RssScrapper(string url, int id)
        {
            this.URL = url;
            this.RssFluxId = id;
            _client = new WebClient();
            _client.Proxy = WebRequest.DefaultWebProxy;
            _client.Proxy.Credentials = CredentialCache.DefaultCredentials;
            _client.Credentials = CredentialCache.DefaultCredentials;

        }



        public void ScrapRss()
        {
            // Read the feed using an XmlReader  
            using (XmlReader reader = XmlReader.Create(_client.OpenRead(URL)))
            {
                var liste = new List<Flux>();
                var feed = SyndicationFeed.Load(reader);
                foreach (var elem in feed.Items)
                {
                    var temp = new Flux()
                    {
                        date = elem.PublishDate.DateTime,
                        ID = RssFluxId,

                        title = elem.Title.Text,
                        text = elem.Summary.Text

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
                using (var sqliteConn = new SQLiteConnection("Data Source=DB\\DBTest.sqlite;Version=3;"))
                {
                    sqliteConn.Open();
                    liste = liste.OrderBy(x => x.date).ToList();
                    var requete =
                        string.Format(
                            "select date from Flux inner join ListeFlux on Flux.IdFlux = ListeFlux.IdFlux  where ListeFlux.Base_Url = '{0}' order by date desc LIMIT  1;",
                            URL);
                    var temp = new SQLiteCommand(requete, sqliteConn);
                    var last = temp.ExecuteReader();
                    if (last.HasRows)
                    {
                        var lastDate = DateTime.Parse(last.GetValues()["date"]);
                        liste = liste.Where(x => x.date > lastDate).ToList();
                    }
                    foreach (var flux in liste)
                    {
                        var tempo = new SQLiteCommand(flux.ToRequest(), sqliteConn);
                        tempo.ExecuteNonQuery();
                    }
                    sqliteConn.Close();
                }
            }
        }
    }
}
