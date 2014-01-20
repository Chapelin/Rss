using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Core;

namespace ServiceRssToDB
{
    public static class Logger
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    ("Logger");

        public static void Log( string message)
        {
            log.Info(message);
        }


        public static void LogFormat(string message, params object[] args)
        {
            log.InfoFormat(message,args);
        }
    }
}
