using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using NLog;

namespace ServiceRssToDB
{
    public class DBManager
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
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

        private SQLiteConnection _connexion;


        private DBManager(string ConnexionString)
        {
            _connexion = new SQLiteConnection(ConnexionString);
            logger.Info("Constructor fini avec la connexionstring {0}",this._connexion.ConnectionString);
        }


        public void Close()
        {
            lock (this)
            {
                this._connexion.Close();
                logger.Info("Connexion fermée");
            }
            
        }

        public DataTable Select(string request)
        {
            try
            {

                DataTable dt = new DataTable();
                lock (this)
                {
                    logger.Info("Select => requete : {0}",request);
                    if (this._connexion.State == ConnectionState.Closed)
                    {
                        this._connexion.Open();
                    }

                    var temp = new SQLiteCommand(request, this._connexion);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(temp);
                    da.Fill(dt);
                    logger.Info("Select ok");
                }
                return dt;
            }
            finally
            {
            }

        }


        public void Insert(string request)
        {
            try
            {
                lock (this)
                {
                    logger.Info("Insert => requete : {0}", request);
                    if (this._connexion.State == ConnectionState.Closed)
                    {
                        this._connexion.Open();
                    }

                    var temp = new SQLiteCommand(request, this._connexion);
                    temp.ExecuteScalar();
                    logger.Info("Insert ok");
                }
            }
            finally
            {
            }
        }











    }
}
