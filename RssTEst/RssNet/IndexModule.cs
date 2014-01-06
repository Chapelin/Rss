using System;
using System.Net;
using System.ServiceModel.Syndication;
using System.Xml;
using Nancy.ModelBinding;
using RssNet.Modeles;

namespace RssNet
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return View["index"];
            };

            Post["/flux"] = parameters =>
                                {
                                    SyndicationFeed feed;
                                    var temp = this.Bind<Flux>();
                                    var url = temp.url;
                                    WebClient client = new WebClient();
                                    client.Proxy = WebRequest.DefaultWebProxy;
                                    client.Proxy.Credentials = CredentialCache.DefaultCredentials;
                                    client.Credentials = CredentialCache.DefaultCredentials;


                                    // Read the feed using an XmlReader  
                                    using (XmlReader reader = XmlReader.Create(client.OpenRead(url)))
                                    {
                                        feed = SyndicationFeed.Load(reader);

                                    }
                                    return View["flux",feed];
                                };
        }
    }
}