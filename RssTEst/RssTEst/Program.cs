using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RssTEst
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            WebClient client = new WebClient();
            client.Proxy = WebRequest.DefaultWebProxy;
            client.Proxy.Credentials = CredentialCache.DefaultCredentials;
            client.Credentials = CredentialCache.DefaultCredentials;


                // Read the feed using an XmlReader  
                using (XmlReader reader = XmlReader.Create(client.OpenRead("http://rss.lemonde.fr/c/205/f/3050/index.rss")))
                {
                    var feed = SyndicationFeed.Load(reader);
                    Console.WriteLine(feed.Id);

                }
            
        }
    }
}