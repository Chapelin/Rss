﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace FaviconGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://www.clubic.com/";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 3000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0";
            request.Method = "GET";
            request.ContentLength = 0;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var doc = new HtmlDocument();
                doc.Load(response.GetResponseStream());
                var node = doc.DocumentNode.SelectSingleNode("//link[@rel='shortcut icon']");
                var urlf =node.Attributes["href"].Value;
              Console.WriteLine("Url trouvée : "+urlf);
              request = (HttpWebRequest)WebRequest.Create(urlf);
              request.Timeout = 3000;
              request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0";
              request.Method = "GET";
              request.ContentLength = 0;
              response = (HttpWebResponse)request.GetResponse();
                StreamToFile(response.GetResponseStream(), @"C:\temp\fav.ico");
                Console.WriteLine("File saved");

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : " + e);
            }
            Console.Read();
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
