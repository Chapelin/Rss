using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using NLog;
using RssEntity;

namespace ServiceRssToDB
{
    public class ContextManager
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private static RssContext _rss;
        public static RssContext Rss
        {
            get
            {
                if (_rss == null)
                {
                    _rss = new RssContext();
                }
                return _rss;
            }
        }
        
    }
}
