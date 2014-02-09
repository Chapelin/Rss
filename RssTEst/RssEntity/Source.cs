using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RssEntity
{
    public class Source
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public string Description { get; set; }
        public string Formatter { get; set; }
        public DateTime DateAjout { get; set; }
        [Required]
        public int Delai { get; set; }
        public virtual List<Categorie> Categories { get; set; } 
        public virtual IEnumerable<Entree> DataFlus { get; set; } 

    }
}
