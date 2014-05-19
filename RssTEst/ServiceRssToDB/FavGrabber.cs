using System;
using System.IO;
using System.Net;
using System.ServiceModel.Syndication;
using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using NLog;
using RssEntity;

namespace ServiceRssToDB
{
    public class FavGrabber
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private Source baseSource;
        private Uri baseUrl;


        public override string ToString()
        {
            return string.Format("Source : {0}  - URL : {1} ", baseSource.Id, baseUrl);
        }

        public FavGrabber(Source baseSource, Uri baseUrl)
        {
            this.baseSource = baseSource;
            this.baseUrl = baseUrl;
        }

        public void GrabFromDirectUrl()
        {
            try
            {
                logger.Info(this + "GrabFromDirectUrl()");
                var response = SendRequest(baseUrl.ToString());
                logger.Info(this + "baseurl ok");
                SaveToDB(response.GetResponseStream(), baseSource.Id);
                baseSource.Favicon = true;
                DBManager.Sources.Save(baseSource);
                logger.Trace(this + "favicon dl direct from url!");
            }
            catch (Exception e)
            {

                logger.Error(this + e.Message);
            }

        }


        public void GrabFromBase()
        {
            try
            {
                Uri urlf;
                logger.Info(this + "GrabFromBase()");
                var response = SendRequest(baseUrl.ToString());
                logger.Info(this + "baseurl ok");
                var doc = new HtmlDocument();
                doc.Load(response.GetResponseStream());
                var node = doc.DocumentNode.SelectSingleNode("//link[@rel='shortcut icon']");

                UriBuilder baseUri =
                    new UriBuilder(response.ResponseUri.ToString().Remove(response.ResponseUri.ToString().LastIndexOf(response.ResponseUri.Query)));
                if (node == null)
                {
                    urlf = new Uri(response.ResponseUri,"/favicon.ico");
                    logger.Info(this + "pas de favicon sur l'url de base, on essaye " + urlf);
                }
                else
                {
                    var target = node.Attributes["href"].Value;
                    target = target.Replace("//", "/");
                    urlf = new Uri(Combine(response.ResponseUri.AbsoluteUri, target));
                    logger.Info(this + string.Format("url favicon {0} : ", urlf));

                }
                response = SendRequest(urlf.AbsoluteUri);
                SaveToDB(response.GetResponseStream(), baseSource.Id);
                baseSource.Favicon = true;
                DBManager.Sources.Save(baseSource);
                logger.Trace(this + "favicon dl !");
            }
            catch (Exception e)
            {

                logger.Error(this + e.Message);
            }



        }


        public static string Combine(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return string.Format("{0}/{1}", uri1, uri2);
        }

        public HttpWebResponse SendRequest(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 3000;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0";
                request.Method = "GET";
                request.ContentLength = 0;

                return (HttpWebResponse)request.GetResponse();
            }
            catch (Exception)
            {
                
                logger.Error(this+" url ratée : "+url);
                throw;
            }
           
        }

        public bool SaveToDB(Stream input, string path)
        {
            var gridFsInfo = DBManager.GridFS.Upload(input, path);
            var fileId = gridFsInfo.Id;
            Favicon fv = new Favicon();
            fv.SourceId = path;
            fv.GridFSId = fileId.AsObjectId.ToString();
            DBManager.Favicon.Insert(fv);
            return true;
        }


    }
}
