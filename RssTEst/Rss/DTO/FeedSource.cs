using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss.DTO
{
    public class FeedSource : BaseDTO
    {
        public string Name { get; set; }

        public DateTime AddedOn { get; set; }

        public string FeedUrl { get; set; }

        public string SiteUrl { get; set; }

        public int Delay { get; set; }

        public string FormatterName { get; set; }

        public Favicon Favicon { get; set; }
    }
}
