﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRssToDB
{
    public  class Downloader
    {

        private static int _parellelDlnb = 1;
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
            Logger.LogFormat("Downloader : debut TryDl : il y a {0} elements dans la queue",_queue.Count);
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
            Logger.LogFormat("Downloader : ajout de {0}",url);
            dlInfoAdded();
        }



        private void Dl(DownloadInfo dl)
        {
            Logger.LogFormat("Downloader : Lancement dl de {0}",dl.Url);
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(dl.Url);
            request.Timeout = 5000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0";
            request.Method = "GET";
            request.ContentLength = 0;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                Task.Factory.StartNew(() => dl.CallBack(response.GetResponseStream()));
                Logger.LogFormat("Downloader : fin dl de {0}", dl.Url);
            }
            catch (Exception e)
            {
                Logger.LogFormat("Downloader : erreur {0} pour {1}",e.Message,dl.Url);
            }
            
            

        }

    }

    internal delegate void Added();
    internal delegate void Finished();


    internal class DownloadInfo
    {
        public  string Url;
        public Action<Stream> CallBack;


    }
}
