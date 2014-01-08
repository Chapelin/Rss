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




        public string ToRequest()
        {
            return "insert into Flux values (" + ID + ", '" + text.Replace("'", "''") + "','" + title.Replace("'", "''") +
                   "','" + image.Replace("'", "''") + "','" + link.Replace("'", "''") + "','" +
                   date.ToString("yy-MM-dd HH:mm:ss") + "');";
        }
    }
}
