using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss.DAL
{
    public interface IGridFsRepository
    {
        string UploadFile(Stream input, string name);
    }
}
