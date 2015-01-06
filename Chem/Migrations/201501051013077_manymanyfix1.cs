namespace Chem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manymanyfix1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reagents", "Reaction_ID", "dbo.Reactions");
            DropIndex("dbo.Reagents", new[] { "Reaction_ID" });
            CreateTable(
                "dbo.ReactionReagents",
                c => new
                    {
                        Reaction_ID = c.Int(nullable: false),
                        Reagent_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Reaction_ID, t.Reagent_ID })
                .ForeignKey("dbo.Reactions", t => t.Reaction_ID, cascadeDelete: true)
                .ForeignKey("dbo.Reagents", t => t.Reagent_ID, cascadeDelete: true)
                .Index(t => t.Reaction_ID)
                .Index(t => t.Reagent_ID);
            
            DropColumn("dbo.Reagents", "Reaction_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reagents", "Reaction_ID", c => c.Int());
            DropIndex("dbo.ReactionReagents", new[] { "Reagent_ID" });
            DropIndex("dbo.ReactionReagents", new[] { "Reaction_ID" });
            DropForeignKey("dbo.ReactionReagents", "Reagent_ID", "dbo.Reagents");
            DropForeignKey("dbo.ReactionReagents", "Reaction_ID", "dbo.Reactions");
            DropTable("dbo.ReactionReagents");
            CreateIndex("dbo.Reagents", "Reaction_ID");
            AddForeignKey("dbo.Reagents", "Reaction_ID", "dbo.Reactions", "ID");
        }
    }
}
