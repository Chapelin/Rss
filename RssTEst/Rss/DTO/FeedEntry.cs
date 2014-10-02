using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss.DTO
{
    public class FeedEntry : BaseDTO
    {
        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string Content { get; set; }

        public FeedSource Source { get; set; }
    }
}
