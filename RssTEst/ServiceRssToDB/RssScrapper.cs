using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using NLog;
using RssEntity;

namespace ServiceRssToDB
{
    public class RssScrapper
    {

        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

        public Source sourceRss;
        private Type FormatterType;
        public override string ToString()
        {
            return String.Format(" IdSource : {0} - Delay : {1} : ", this.sourceRss.Id, this.sourceRss.Delai);
        }

        public RssScrapper( Source id)
        {

            this.sourceRss = id;
            if (!string.IsNullOrWhiteSpace(sourceRss.Formatter))
                FormatterType = Assembly.Load("Readers").CreateInstance("Readers." + sourceRss.Formatter).GetType();


            logger.Info(this + "initialized");

        }


        public void Launch()
        {
            logger.Info(this + "Launched");
            DateTime next;
            while (true)
            {
                next = DateTime.Now.AddMilliseconds(this.sourceRss.Delai * 1000);
                Downloader.Instance.Add(this.sourceRss.URL, ScrapRss);
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
                    if (!sourceRss.Favicon)
                    {
                        //s'il y a un imageurl de prévu on ta tente
                        if (feed.ImageUrl != null)
                        {
                            var baseurl = feed.ImageUrl;
                            logger.Info(this + " Lancement du grab  direct favicon sur " + baseurl);
                            FavGrabber grab = new FavGrabber(sourceRss, baseurl);
                            Task.Factory.StartNew(grab.GrabFromDirectUrl);
                        }
                        else if (feed.Links != null && feed.Links.Count > 0) //sinon s'il y a des liens, on essaye de recuperer le favicon depuis le premier
                        {
                            var baseurl = feed.Links[0].Uri;
                            logger.Info(this + " Lancement du grab favicon sur " + baseurl);
                            FavGrabber grab = new FavGrabber(sourceRss, baseurl);
                            Task.Factory.StartNew(grab.GrabFromBase);
                        }
                    }
                    foreach (var elem in feed.Items)
                    {
                        try
                        {
                            var textemp = elem.Summary == null ? string.Empty : elem.Summary.Text;
                            if (textemp.Count() > 3999)
                                textemp = textemp.Substring(0, 3999);
                            var temp = new Entree()
                            {
                                Date = elem.PublishDate.DateTime,
                                SourceId = sourceRss.Id,

                                Titre = elem.Title == null ? string.Empty : elem.Title.Text,
                                Texte = textemp,
                                DateInsertion = DateTime.Now,
                                Link = "",
                                Image = "",
                                UniqId = elem.Id

                            };
                            if (elem.Links.Count > 0)
                            {
                                temp.Link = elem.Links[0].GetAbsoluteUri().ToString();
                            }
                            if (elem.Links.Count > 1)
                            {
                                temp.Image = elem.Links[1].GetAbsoluteUri().ToString();
                            }
                            if (string.IsNullOrWhiteSpace(temp.UniqId))
                            {
                                if (!string.IsNullOrWhiteSpace(temp.Link))
                                    temp.UniqId = temp.Link;
                                else
                                {
                                    temp.UniqId = temp.Date.ToString();
                                }
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
                    liste = liste.OrderByDescending(x => x.Date).ToList();

                    //on ne recupere que les 100 dernieres entréées
                    MongoCursor<Entree> col = DBManager.Entrees.Find(Query<Entree>.EQ(x => x.SourceId, this.sourceRss.Id)).SetSortOrder(SortBy.Descending("Date")).SetLimit(100);

                    if (col != null && col.Any())
                    {
                        //et on compare les uid
                        var listUid = col.Select(x => x.UniqId);
                        liste = liste.Where(x => !listUid.Contains(x.UniqId)).ToList();
                    }
                    logger.Info(this + "nombre d'entrée à inserer {0}", liste.Count);
                    if(liste.Count>0)
                        DBManager.Entrees.InsertBatch(liste);
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
