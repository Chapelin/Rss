using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace Readers
{
    public class Rss20FeedFormatter : SyndicationFeedFormatter
    {
        public override bool CanRead(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            return reader.IsStartElement("rss", "") ; 
        }

       
        public virtual string NamespaceUri
        {
            get { return ""; }
        } 


        public override void ReadFrom(XmlReader reader)
        {
            if (!this.CanRead(reader))
            {
                throw new XmlException("Unknown RSS 20 feed format.");
            }
            
            this.ReadFeed(reader);
           
        }


        private void ReadFeed(XmlReader reader)
        {
            this.SetFeed(this.CreateFeedInstance());
            this.ReadXml(reader);
        }

        private void ReadXml(XmlReader reader)
        {
            var list = new List<SyndicationItem>();
            reader.ReadStartElement("rss");    // Read in <channel>
            reader.ReadStartElement("channel");    // Read in <channel>
            while (reader.IsStartElement())       // Process <channel> children
            {
                if (reader.IsStartElement("title"))
                    Feed.Title = new TextSyndicationContent(reader.ReadElementString());
                else if (reader.IsStartElement("link"))
                    Feed.Links.Add(new SyndicationLink(new Uri(reader.ReadElementString())));
                else if (reader.IsStartElement("description"))
                    Feed.Description = new TextSyndicationContent(reader.ReadElementString());
                else if (reader.IsStartElement("image"))
                {
                    reader.ReadStartElement("image");
                    while (reader.IsStartElement())
                    {
                        if (reader.IsStartElement("url"))
                        {
                            Feed.ImageUrl = new Uri(reader.ReadElementString());
                        }
                        else
                        {
                            reader.Skip();
                        }
                    }
                    reader.ReadEndElement();
                    
                }
                else if (reader.IsStartElement("language"))
                    Feed.Language = reader.ReadElementString();
                else if (reader.IsStartElement("item"))
                {
                    SyndicationItem item = new SyndicationItem();
                    reader.ReadStartElement("item");
                    while (reader.IsStartElement())
                    {
                        if (reader.IsStartElement("title"))
                        {
                            item.Title = new TextSyndicationContent(reader.ReadElementString());
                        }
                        else if (reader.IsStartElement("link"))
                        {
                            item.Links.Add(new SyndicationLink(new Uri(reader.ReadElementString())));
                        }
                        else if (reader.IsStartElement("description"))
                        {
                            item.Summary = new TextSyndicationContent(reader.ReadElementString());
                        }
                        else if (reader.IsStartElement("comments"))
                        {
                            //seulement si pas deja une description
                            if (item.Summary == null)
                                item.Summary = new TextSyndicationContent(reader.ReadElementString());
                        }
                        else if (reader.IsStartElement("guid"))
                        {
                            item.Id = reader.ReadElementString();
                        }
                        else if (reader.IsStartElement("pubDate"))
                        {
                            DateTime res;
                            var temp = reader.ReadElementString();
                            if (!DateTime.TryParse(temp, out res))
                            {

                                res = DateTime.Now.Date;
                            }

                            item.PublishDate = res;

                        }
                        else
                        {
                            reader.Skip();
                        }

                    }


                    reader.ReadEndElement();
                    list.Add(item);
                }
                else
                    reader.Skip();
            }
            reader.ReadEndElement();            // Read in </channel> 
         
            Feed.Items = list;
        }

        public override void WriteTo(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        protected override SyndicationFeed CreateFeedInstance()
        {
            return new SyndicationFeed();
        }

        public override string Version
        {
            get { return "Rss20"; }
        }
    }
}
