using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss.DAL.Implementation
{
    public class GridFsRepository : IGridFsRepository
    {
        private IRssRepository _rssRepository;

        public GridFsRepository(IRssRepository rssRepository)
        {
            _rssRepository = rssRepository;
        }

        public string UploadFile(Stream input, string name)
        {
            throw new NotImplementedException();
        }
    }
}
