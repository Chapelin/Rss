using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss.DTO
{
    public class Favicon : BaseDTO
    {
        public FeedSource SourceAssociated { get; set; }

        public int GridFSId { get; set; }
    }
}
