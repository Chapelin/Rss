namespace RssEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClefEntree : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entrees",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Texte = c.String(maxLength: 4000),
                    Titre = c.String(maxLength: 4000),
                    Image = c.String(maxLength: 4000),
                    Link = c.String(maxLength: 4000),
                    Date = c.DateTime(nullable: false),
                    DateInsertion = c.DateTime(nullable: false),
                    Source_ID = c.Int(),
                })
                .PrimaryKey(t => new {t.ID, t.Source_ID})
                .ForeignKey("dbo.Sources", t => t.Source_ID)
                .Index(t => t.Source_ID);
            
        }
        
        public override void Down()
        {
        }
    }
}
