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
using NLog;
using RssEntity;

namespace ServiceRssToDB
{
    public class RssScrapper
    {

        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

        public string URL { get; set; }
        public Source RssId { get; set; }
        private double Delay_seconds;
        private Type FormatterType;
        public override string ToString()
        {
            return String.Format(" Id : {0} - Delay : {1} - URL : {2} : ", this.RssId,this.Delay_seconds,
                this.URL);
        }

        public RssScrapper(string url, Source id, double delay, string formatterType = null)
        {

            this.URL = url;
            this.RssId = id;
            this.Delay_seconds = delay;
            if (!string.IsNullOrWhiteSpace(formatterType))
                FormatterType = Assembly.Load("Readers").CreateInstance("Readers." + formatterType).GetType();


            logger.Info(this + "initialized");

        }


        public void Launch()
        {
            logger.Info(this + "Launched");
            DateTime next;
            while (true)
            {
                next = DateTime.Now.AddMilliseconds(Delay_seconds * 1000);
                Downloader.Instance.Add(this.URL,ScrapRss);
                var toSleep = next - DateTime.Now;
                if (toSleep.TotalMilliseconds > 0)
                    Thread.Sleep(toSleep);
            }

        }

        public void ScrapRss(Stream response)
        {

           
            SyndicationFeed feed = null;

            try
            {
                // Read the feed using an XmlReader  
                using (XmlReader reader = XmlReader.Create(response))
                {
                    logger.Info(this + "url OK");
                    var liste = new List<Entree>();
                    //formatter custom
                    if (FormatterType != null)
                    {
                        var constr = FormatterType.GetConstructor(new Type[] { });
                        var tempo = (SyndicationFeedFormatter)constr.Invoke(new object[] { });
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
                            var temp = new Entree()
                            {
                                Date = elem.PublishDate.DateTime,
                                Source = RssId,

                                Titre = elem.Title == null ? string.Empty : elem.Title.Text,
                                Texte = elem.Summary == null ? string.Empty : elem.Summary.Text,
                                DateInsertion = DateTime.Now,
                                Link = "",
                                Image = ""


                            };
                            if (elem.Links.Count > 0)
                            {
                                temp.Link = elem.Links[0].GetAbsoluteUri().ToString();
                            }
                            if (elem.Links.Count > 1)
                            {
                                temp.Image = elem.Links[1].GetAbsoluteUri().ToString();
                            }
                            liste.Add(temp);
                        }
                        catch (Exception e)
                        {
                            logger.Info(this + "Erreur sur un element");
                            throw;
                        }

                    }
                    logger.Info(this + "Info recuperées, en attente mise en base");
                    liste = liste.OrderBy(x => x.Date).ToList();
                   

                    var last = RssId.DataFlus.OrderByDescending(x => x.Date);
                    if (last.Any())
                    {

                        var lastDate = last.First().Date;
                        liste = liste.Where(x => x.Date > lastDate).ToList();

                    }
                    logger.Info(this + "nombre d'entrée à inserer {0}", liste.Count);
                        ContextManager.Rss.Datas.AddRange(liste);

                    logger.Info(this + "Scrap ok ");
                }
            }
            catch (Exception e)
            {
                logger.Info(this + "Exception RssScrapper : {0}", e.GetType());
            }

        }
      
    }
}
