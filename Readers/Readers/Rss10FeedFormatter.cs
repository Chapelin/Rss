using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace Readers
{
    public class Rss10FeedFormatter : SyndicationFeedFormatter
    {
        public override bool CanRead(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            return reader.IsStartElement("RDF", this.RdfNamespaceUri); 
        }

        public virtual string RdfNamespaceUri
        {
            get { return "http://www.w3.org/1999/02/22-rdf-syntax-ns#"; }
        }

        public virtual string NamespaceUri
        {
            get { return "http://purl.org/rss/1.0/"; }
        } 


        public override void ReadFrom(XmlReader reader)
        {
            if (!this.CanRead(reader))
            {
                throw new XmlException("Unknown RSS 1.0 feed format.");
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
            reader.ReadStartElement("rdf:RDF");    // Read in <channel>
            reader.ReadStartElement("channel");    // Read in <channel>
            while (reader.IsStartElement())       // Process <channel> children
            {
                if (reader.IsStartElement("title"))
                    Feed.Title = new TextSyndicationContent(reader.ReadElementString());
                else if (reader.IsStartElement("link"))
                    Feed.Links.Add(new SyndicationLink(new Uri(reader.ReadElementString())));
                else if (reader.IsStartElement("description"))
                    Feed.Description = new TextSyndicationContent(reader.ReadElementString());
                else if(reader.IsStartElement("generator"))
                    Feed.Generator = reader.ReadElementString();
                else if (reader.IsStartElement("language"))
                    Feed.Language = reader.ReadElementString();
                else
                    reader.Skip();
            }
            reader.ReadEndElement();            // Read in </channel> 
            var list = new List<SyndicationItem>();
            while (reader.IsStartElement("item"))
            {
                SyndicationItem item = new SyndicationItem();
                reader.ReadStartElement("item");
                while (reader.IsStartElement())
                {
                    if (reader.IsStartElement("title"))
                    {
                        item.Title = new TextSyndicationContent(reader.ReadElementString());
                    }
                    else if(reader.IsStartElement("link"))
                    {
                        item.Links.Add(new SyndicationLink(new Uri(reader.ReadElementString())));
                    }
                    else if(reader.IsStartElement("description"))
                    {
                        item.Summary = new TextSyndicationContent(reader.ReadElementString());
                    }
                    else if(reader.IsStartElement("guid"))
                    {
                        item.Id = reader.ReadElementString();
                    }
                    else if (reader.IsStartElement("content:encoded"))
                    {
                        item.Content = new TextSyndicationContent(reader.ReadElementString());
                    }
                    else if (reader.IsStartElement("pubDate"))
                    {
                        item.PublishDate = new DateTimeOffset(DateTime.Parse(reader.ReadElementString()));
                    }
                    else
                    {
                        reader.Skip();
                    }
                    
                }
                reader.ReadEndElement();
                list.Add(item);
            }
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
            get { return "Rss10"; }
        }
    }
}
