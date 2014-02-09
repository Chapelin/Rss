using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RssEntity
{
    public class Categorie
    {
         [Key]
        public int ID { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual List<Source> FluxAssocies { get; set; } 
    }
}
