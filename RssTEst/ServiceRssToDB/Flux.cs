using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceRssToDB
{
    public class Flux
    {
        public int ID { get; set; }
        public string text { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string link { get; set; }
        public DateTime date { get; set; }
        public DateTime dateInsert { get; set; }




        public string ToRequest()
        {
            return "insert into Flux values ('" + dateInsert.ToSqlLiteFormat() + "'," + ID + ", '" + text.Replace("'", "''") + "','" + title.Replace("'", "''") +
                   "','" + image.Replace("'", "''") + "','" + link.Replace("'", "''") + "','" +
                   date.ToSqlLiteFormat() + "');";
        }
    }
}
