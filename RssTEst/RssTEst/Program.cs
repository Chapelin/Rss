using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Data.SQLite;
using Readers;
using RssEntity;
using ServiceRssToDB;

namespace RssTEst
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("Go !");
               // InitSources();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.GetType());
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                Console.WriteLine("OK");
                Console.Read();
            }


        }

        private static void InitSources()
        {
            var res = DBManager.Rss.GetCollection<Source>("Sources");
            var temp = new List<object[]>();
            temp.Add(new Object[]
            {"", "Le monde - International", 120, "http://rss.lemonde.fr/c/205/f/3050/index.rss", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {"", "SebSauvage - shaarli", 600, "http://sebsauvage.net/links/?do=rss", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[] {"", "PC inpact", 300, "http://www.pcinpact.com/rss/news.xml", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {"", "Clubic", 300, "http://com.clubic.feedsportal.com/c/33464/f/581979/index.rss", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[] {"", "CDH", 300, "http://www.comptoir-hardware.com/home.xml", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {"", "SMBC", 21600, "http://feeds.feedburner.com/smbc-comics/PvLb?format=xml", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[] {"", "Reflet", 21600, "http://reflets.info/feed/", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[] {"", "Explosm", 21600, "http://feeds.feedburner.com/Explosm", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {"", "orben", 3600, "http://feeds.feedburner.com/KorbensBlog-UpgradeYourMind?format=xml", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {"", "DealLabs.com - Deals nouveaux", 120, "http://www.dealabs.com/rss/new.xml", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {
                "", "This is why i'm broke", 3600, "http://feeds.feedburner.com/ThisIsWhyImBroke?format=xml",
                "2014-02-09 16:38:27.313"
            });
            temp.Add(new Object[]
            {"", "The big Picture", 43200, "http://feeds.boston.com/boston/bigpicture/index", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {
                "Rss10FeedFormatter", "OatMeal", 21600, "http://feeds.feedburner.com/oatmealfeed?format=xml",
                "2014-02-09 16:38:27.313"
            });
            temp.Add(new Object[] {"", "XKCD", 21600, "https://xkcd.com/rss.xml", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[] {"", "Direct atin", 120, "http://www.directmatin.fr/rss.xml", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {"Rss10FeedFormatter", "Steam", 600, "http://store.steampowered.com/feeds/news.xml", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[] {"", "V-inc", 3600, "http://widget.stagram.com/rss/n/vinc_fr/", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {"", "google actu", 45, "http://news.google.fr/news?pz=1&cf=all&ned=fr&hl=fr&output=rss", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {
                "", "iberation", 120, "http://liberation.fr.feedsportal.com/c/32268/fe.ed/rss.liberation.fr/rss/9/",
                "2014-02-09 16:38:27.313"
            });
            temp.Add(new Object[]
            {"", "Figaro", 120, "http://feeds.lefigaro.fr/c/32266/f/438191/index.rss", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {
                "", "Nouvel Obs", 120, "http://rss.nouvelobs.com/c/32262/fe.ed/tempsreel.nouvelobs.com/rss.xml",
                "2014-02-09 16:38:27.313"
            });
            temp.Add(new Object[]
            {"", "Rue 89", 600, "http://rue89.feedsportal.com/c/33822/f/608959/index.rss", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[] {"", "Le point", 120, "http://www.lepoint.fr/rss.xml", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[] {"", "StackOverflow", 120, "http://stackoverflow.com/feeds", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {
                "Rss20FeedFormatter", "Monsieur le hien", 21600, "http://www.monsieur-le-chien.fr/rss.php",
                "2014-02-09 16:38:27.313"
            });
            temp.Add(new Object[] {"", "DTC", 300, "http://feeds.feedburner.com/bashfr", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {"", "Hollandais Volant", 600, "http://lehollandaisvolant.net/rss.php", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[] {"", "Numerama", 300, "http://www.numerama.com/rss/news.rss", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {
                "", "NY Times - International", 120, "http://rss.nytimes.com/services/xml/rss/nyt/InternationalHome.xml",
                "2014-02-09 16:38:27.313"
            });
            temp.Add(new Object[] {"", "Sam & max", 3600, "http://sametmax.com/feed/", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[] {"", "VDM", 300, "http://feedpress.me/viedemerde", "2014-02-09 16:38:27.313"});
            temp.Add(new Object[]
            {"", "Torrent411 - Derniers Films", 150, "http://www.t411.me/rss/?cat=631", "2014-02-09 16:38:27.313"});

            foreach (var objectse in temp)
            {
                Source s = new Source()
                {
                    DateAjout = DateTime.Now,
                    Delai = (int) objectse[2],
                    Description = (string) objectse[1],
                    Formatter = (string) objectse[0],
                    URL = (string) objectse[3]
                };
                res.Insert(s);
            }
        }
    }
}
