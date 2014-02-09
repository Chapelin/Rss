namespace RssEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MajData : DbMigration
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sources", t => t.Source_ID)
                .Index(t => t.Source_ID);
            
           
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entrees", "Source_ID", "dbo.Sources");
            DropForeignKey("dbo.SourceCategories", "Categorie_ID", "dbo.Categories");
            DropForeignKey("dbo.SourceCategories", "Source_ID", "dbo.Sources");
            DropIndex("dbo.Entrees", new[] { "Source_ID" });
            DropIndex("dbo.SourceCategories", new[] { "Categorie_ID" });
            DropIndex("dbo.SourceCategories", new[] { "Source_ID" });
            DropTable("dbo.SourceCategories");
            DropTable("dbo.Entrees");
            DropTable("dbo.Sources");
            DropTable("dbo.Categories");
        }
    }
}
