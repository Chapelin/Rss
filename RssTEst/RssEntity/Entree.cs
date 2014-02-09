using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RssEntity
{
    public class Entree
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Texte { get; set; }
        public string Titre { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateInsertion { get; set; }
        [Key]
        public Source Source { get; set; }
    }
}
