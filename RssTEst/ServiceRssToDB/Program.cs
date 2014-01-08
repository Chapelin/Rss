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
    class Program
    {
        static void Main(string[] args)
        {

            SyndicationFeed feed;
            WebClient client = new WebClient();
            client.Proxy = WebRequest.DefaultWebProxy;
            client.Proxy.Credentials = CredentialCache.DefaultCredentials;
            client.Credentials = CredentialCache.DefaultCredentials;


            // Read the feed using an XmlReader  
            using (XmlReader reader = XmlReader.Create(client.OpenRead("http://rss.lemonde.fr/c/205/f/3052/index.rss")))
            {
                var liste = new List<Flux>();
                feed = SyndicationFeed.Load(reader);
                foreach (var elem in feed.Items)
                {
                    var temp = new Flux()
                                   {
                                       date = elem.PublishDate.DateTime,
                                       ID = 1,

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
