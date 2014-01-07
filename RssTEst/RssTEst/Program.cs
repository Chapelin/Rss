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
            using (var sqliteConn = new SQLiteConnection("Data Source=DBTest.sqlite;Version=3;"))
            {
                sqliteConn.Open();
                string requete =
                    "create table Flux (id int(10), text varchar(2048), title varchar(2048), image varchar (2048), link varchar(2048), date Datetime) ";
                var sqlLiteComm = new SQLiteCommand(requete, sqliteConn);
                sqlLiteComm.ExecuteNonQuery();

                var comtest =
                    "insert into Flux values (1,'Je test le contenu d''un flux lolololil les sarrasins sont dans le champs de blé, je repete lol kikoo','Titre !','http://www.google.fr/img','http://www.google.fr',date('now'))";
                sqlLiteComm = new SQLiteCommand(comtest,sqliteConn);
                sqlLiteComm.ExecuteNonQuery();
                sqliteConn.Close();

            }
            
        }
    }
}