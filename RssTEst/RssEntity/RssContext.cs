using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace RssEntity
{
    public class RssContext : DbContext
    {
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Entree> Datas { get; set; }

        public RssContext()
            : base("DataContext")
        {
            
        }
    }
}
