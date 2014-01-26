using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ServiceRssToDB
{
    public  class Downloader
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private static int _parellelDlnb = 5;
        private static int _actualDlnb = 0;
        private static Downloader _singleton;

        public static Downloader Instance
        {
            get
            {
                if(_singleton==null)
                    _singleton = new Downloader();
                return _singleton;
            }
        }


        private  static Queue<DownloadInfo> _queue = new Queue<DownloadInfo>();


        private event Added dlInfoAdded;
        private event Finished dlInfofinished;


        internal Downloader()
        {
            this.dlInfoAdded += TryDl;
            this.dlInfofinished += TryDl;
        }

        private void TryDl()
        {
            logger.Info("Debut TryDl : il y a {0} elements dans la queue",_queue.Count);
            if (_parellelDlnb > _actualDlnb && _queue.Count>0)
            {
                    _actualDlnb++;
                var todo = _queue.Dequeue();

                Task.Factory.StartNew(() =>
                {
                    Dl(todo);
                    _actualDlnb--;
                    dlInfofinished();
                });
            }
           
   
        }

        public void Add(string url, Action<Stream> todo)
        {

            _queue.Enqueue(new DownloadInfo(){Url = url,CallBack = todo});
            logger.Info("Ajout de {0}",url);
            dlInfoAdded();
        }



        private void Dl(DownloadInfo dl)
        {
            logger.Info("Lancement dl de {0}",dl.Url);
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(dl.Url);
            request.Timeout = 3000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0";
            request.Method = "GET";
            request.ContentLength = 0;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                Task.Factory.StartNew(() => dl.CallBack(response.GetResponseStream()));
                logger.Info("Fin dl de {0}", dl.Url);
            }
            catch (Exception e)
            {
                logger.Error("Downloader : erreur {0} pour {1}",e.Message,dl.Url);
                dl.retryLeft--;
                if(dl.retryLeft>0)
                    _queue.Enqueue(dl);
                else
                {
                    logger.Error("Plus de retry possible pour {0}", dl.Url);
                }
            }
            
            

        }

    }

    internal delegate void Added();
    internal delegate void Finished();


    internal class DownloadInfo
    {
        public  string Url;
        public Action<Stream> CallBack;
        public int retryLeft;

        public DownloadInfo()
        {
            retryLeft = 3;
        }


    }
}
