﻿using System;
using System.Collections.Generic;
using System.Data;
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

        private SQLiteConnection _connexion;


        private DBManager(string ConnexionString)
        {
            _connexion = new SQLiteConnection(ConnexionString);
        }


        public void Close()
        {
            lock (this)
            {
                this._connexion.Close();
            }
            
        }

        public DataTable Select(string request)
        {
            try
            {

                DataTable dt = new DataTable();
                lock (this)
                {
                    if (this._connexion.State == ConnectionState.Closed)
                    {
                        this._connexion.Open();
                    }

                    var temp = new SQLiteCommand(request, this._connexion);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(temp);
                    da.Fill(dt);
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
                    if (this._connexion.State == ConnectionState.Closed)
                    {
                        this._connexion.Open();
                    }

                    var temp = new SQLiteCommand(request, this._connexion);
                    temp.ExecuteScalar();
                }
            }
            finally
            {
            }
        }











    }
}
