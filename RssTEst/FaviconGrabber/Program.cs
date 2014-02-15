using System;
using System.Collections.Generic;
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
            string url = "http://www.lemonde.fr";
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
                var urlf =node.Attributes["href"];
              Console.WriteLine("Url trouvée : "+urlf);


            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : " + e);
            }
            Console.Read();
        }
    }
}
