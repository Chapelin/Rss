using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ServiceRssToDB
{
    public class DBManager
    {
        private static DBManager _manager;
        public static DBManager Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = new DBManager("Data Source=DB\\DBTest.sqlite;Version=3;");
                }
                return _manager;
            }
        }

        private SQLiteConnection Connexion;


        public DBManager(string ConnexionString)
        {
            Connexion = new SQLiteConnection(ConnexionString);
        }



        






    }
}
