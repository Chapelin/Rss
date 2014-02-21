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
        private SyndicationLink baseUrl;


        public override string ToString()
        {
            return string.Format("Source : {0}  - URL : {1} ", baseSource.Id, baseUrl.Uri);
        }

        public FavGrabber(Source baseSource, SyndicationLink baseUrl)
        {
            this.baseSource = baseSource;
            this.baseUrl = baseUrl;
        }

        public void Grab()
        {
            try
            {
                Uri urlf;
                logger.Info(this + "Init()");
                var response = SendRequest(baseUrl.Uri.ToString());
                logger.Info(this + "baseurl ok");
                var doc = new HtmlDocument();
                doc.Load(response.GetResponseStream());
                var node = doc.DocumentNode.SelectSingleNode("//link[@rel='shortcut icon']");
                UriBuilder baseUri =
                    new UriBuilder(baseUrl.GetAbsoluteUri().ToString().Remove(baseUrl.GetAbsoluteUri().ToString().LastIndexOf(baseUrl.GetAbsoluteUri().Query)));
                if (node == null)
                {
                    urlf = new Uri(baseUri.Uri,"/favicon.ico");
                    logger.Info(this + "pas de favicon sur l'url de base, on essaye " + urlf);
                }
                else
                {
                    urlf = new Uri(baseUri.Uri, baseUri.Uri.LocalPath + "/" + node.Attributes["href"].Value);
                    logger.Info(this + string.Format("url favicon {0} : ", urlf));

                }
                response = SendRequest(urlf.AbsoluteUri);
                SaveToDB(response.GetResponseStream(), baseSource.Id);
                baseSource.Favicon = true;
                DBManager.Sources.Save(baseSource);
                logger.Info(this + "favicon dl !");
            }
            catch (Exception e)
            {

                logger.Error(this + e.Message);
            }



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
            catch (Exception e )
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
