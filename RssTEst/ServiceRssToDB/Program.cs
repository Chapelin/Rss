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
                        image = elem.Links[1].GetAbsoluteUri().ToString(),
                        link = elem.Links[0].GetAbsoluteUri().ToString(),
                        title = elem.Title.Text,
                        text = elem.Summary.Text

                    };
                    liste.Add(temp);
                }
                using (var sqliteConn = new SQLiteConnection("Data Source=DBTest.sqlite;Version=3;"))
                {
                    sqliteConn.Open();
                    foreach (var flux in liste)
                    {
                        new SQLiteCommand(flux.ToRequest(), sqliteConn).ExecuteNonQuery();
                    }
                    sqliteConn.Close();
             
                }

            }
        }
    }
}
