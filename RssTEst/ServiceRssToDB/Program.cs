using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ServiceRssToDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new ScrappersManager();
            manager.InitListeScrap();
            Task.Factory.StartNew(manager.Scrap);
            Console.ReadLine();
            DBManager.Manager.Close();
            //var temp = new RssScrapper("http://rss.lemonde.fr/c/205/f/3052/index.rss", 2,60);
            //var t =Task.Factory.StartNew(temp.Launch);
            //Task.WaitAll(t);

        }
    }

    public static class DateTimeExtension
    {
        public static string ToSqlLiteFormat(this DateTime d)
        {
            return d.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
