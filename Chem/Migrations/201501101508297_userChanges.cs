namespace Chem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "AddedBy_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Reactions", "AddedBy_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Reagents", "AddedBy_UserId", "dbo.UserProfile");
            DropIndex("dbo.Movies", new[] { "AddedBy_UserId" });
            DropIndex("dbo.Reactions", new[] { "AddedBy_UserId" });
            DropIndex("dbo.Reagents", new[] { "AddedBy_UserId" });
            AddColumn("dbo.Movies", "AddedById", c => c.Int(nullable: false));
            AddColumn("dbo.Reactions", "AddedById", c => c.Int(nullable: false));
            AddColumn("dbo.Reagents", "AddedById", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "AddedBy_UserId");
            DropColumn("dbo.Reactions", "AddedBy_UserId");
            DropColumn("dbo.Reagents", "AddedBy_UserId");
            DropTable("dbo.UserProfile");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            AddColumn("dbo.Reagents", "AddedBy_UserId", c => c.Int());
            AddColumn("dbo.Reactions", "AddedBy_UserId", c => c.Int());
            AddColumn("dbo.Movies", "AddedBy_UserId", c => c.Int());
            DropColumn("dbo.Reagents", "AddedById");
            DropColumn("dbo.Reactions", "AddedById");
            DropColumn("dbo.Movies", "AddedById");
            CreateIndex("dbo.Reagents", "AddedBy_UserId");
            CreateIndex("dbo.Reactions", "AddedBy_UserId");
            CreateIndex("dbo.Movies", "AddedBy_UserId");
            AddForeignKey("dbo.Reagents", "AddedBy_UserId", "dbo.UserProfile", "UserId");
            AddForeignKey("dbo.Reactions", "AddedBy_UserId", "dbo.UserProfile", "UserId");
            AddForeignKey("dbo.Movies", "AddedBy_UserId", "dbo.UserProfile", "UserId");
        }
    }
}
