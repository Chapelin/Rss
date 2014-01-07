using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Data.SQLite;

namespace RssTEst
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SQLiteConnection.CreateFile("DBTest.sqlite");
            
        }
    }
}