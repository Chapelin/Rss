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
        public DbSet<SourceFlux> Sources { get; set; }
        public DbSet<DataFlux> Datas { get; set; }
    }
}
