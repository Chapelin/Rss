using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RssEntity
{
    public class DataFlux
    {
        [Key]
        public int ID { get; set; }
        public string Texte { get; set; }
        public string Titre { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateInsertion { get; set; }
        public SourceFlux Source { get; set; }
    }
}
