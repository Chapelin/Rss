using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Data.SQLite;
using Readers;

namespace RssTEst
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Read in the RSS 1.0 feed from the specified URL
            XmlReader reader = XmlReader.Create("http://feeds.feedburner.com/oatmealfeed?format=xml");

            // Create a new Rss10FeedFormatter object
            Rss10FeedFormatter formatter = new Rss10FeedFormatter();

            // Parse the feed!
            formatter.ReadFrom(reader);
            ;
            Console.ReadLine();

        }
    }
}