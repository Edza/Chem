namespace Chem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Desc = c.String(),
                        AddedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserProfile", t => t.AddedBy_UserId)
                .Index(t => t.AddedBy_UserId);
            
            AddColumn("dbo.Reagents", "Reaction_ID", c => c.Int());
            AddForeignKey("dbo.Reagents", "Reaction_ID", "dbo.Reactions", "ID");
            CreateIndex("dbo.Reagents", "Reaction_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reactions", new[] { "AddedBy_UserId" });
            DropIndex("dbo.Reagents", new[] { "Reaction_ID" });
            DropForeignKey("dbo.Reactions", "AddedBy_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Reagents", "Reaction_ID", "dbo.Reactions");
            DropColumn("dbo.Reagents", "Reaction_ID");
            DropTable("dbo.Reactions");
        }
    }
}
