namespace Chem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
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
            AddForeignKey("dbo.Reagents", "AddedBy_UserId", "dbo.UserProfile", "UserId");
            CreateIndex("dbo.Reagents", "AddedBy_UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reagents", new[] { "AddedBy_UserId" });
            DropForeignKey("dbo.Reagents", "AddedBy_UserId", "dbo.UserProfile");
            DropColumn("dbo.Reagents", "AddedBy_UserId");
            DropTable("dbo.UserProfile");
        }
    }
}
