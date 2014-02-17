using System;
using System.IO;
using System.Net;
using System.ServiceModel.Syndication;
using HtmlAgilityPack;
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
            return string.Format("Source : {0}  - URL : {1}", baseSource.Id, baseUrl);
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
                logger.Info(this + " Init()");
                var response = SendRequest(baseUrl.Uri.ToString());
                logger.Info(this + " baseurl ok");
                var doc = new HtmlDocument();
                doc.Load(response.GetResponseStream());
                var node = doc.DocumentNode.SelectSingleNode("//link[@rel='shortcut icon']");
                UriBuilder baseUri =
                    new UriBuilder(baseUrl.GetAbsoluteUri().ToString().Remove(baseUrl.GetAbsoluteUri().ToString().LastIndexOf(baseUrl.GetAbsoluteUri().PathAndQuery)));
                if (node == null)
                {
                    urlf = new Uri(baseUri.Uri,"/favicon.ico");
                    logger.Info(this + " pas de favicon sur l'url de base, on essaye " + urlf);
                }
                else
                {
                    urlf = new Uri(baseUri.Uri, node.Attributes["href"].Value);
                    logger.Info(this + string.Format(" url favicon {0} : ", urlf));

                }
                response = SendRequest(urlf.AbsoluteUri);
                StreamToFile(response.GetResponseStream(), @"C:\PERSO_GIT\Trombi\Rss\RssTEst\RssApi\Images\fav\" + baseSource.Id + ".ico");
                baseSource.Favicon = true;
                DBManager.Sources.Save(baseSource);
                logger.Info(this + " favicon dl !");
            }
            catch (Exception e)
            {

                logger.Error(e.Message);
            }



        }

        public HttpWebResponse SendRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 3000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0";
            request.Method = "GET";
            request.ContentLength = 0;

            return (HttpWebResponse)request.GetResponse();
        }

        public static bool StreamToFile(Stream input, string path)
        {
            var retour = true;
            int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];
            try
            {
                using (FileStream fs = new FileStream(path,
                                FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    int read;
                    while (true)
                    {
                        read = input.Read(buffer, 0, buffer.Length);
                        if (read > 0)
                        {
                            fs.Write(buffer, 0, read);
                        }
                        else
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
            }
            return retour;
        }


    }
}
