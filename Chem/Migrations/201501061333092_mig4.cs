namespace Chem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReactionReagents", "Reaction_ID", "dbo.Reactions");
            DropForeignKey("dbo.ReactionReagents", "Reagent_ID", "dbo.Reagents");
            DropIndex("dbo.ReactionReagents", new[] { "Reaction_ID" });
            DropIndex("dbo.ReactionReagents", new[] { "Reagent_ID" });
            CreateTable(
                "dbo.ReagentReactions",
                c => new
                    {
                        Reagent_ID = c.Int(nullable: false),
                        Reaction_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Reagent_ID, t.Reaction_ID })
                .ForeignKey("dbo.Reagents", t => t.Reagent_ID, cascadeDelete: true)
                .ForeignKey("dbo.Reactions", t => t.Reaction_ID, cascadeDelete: true)
                .Index(t => t.Reagent_ID)
                .Index(t => t.Reaction_ID);
            
            AddColumn("dbo.Movies", "Desc", c => c.String());
            AddColumn("dbo.Movies", "Url", c => c.String());
            AddColumn("dbo.Movies", "Reaction_ID", c => c.Int());
            AddColumn("dbo.Movies", "AddedBy_UserId", c => c.Int());
            AddForeignKey("dbo.Movies", "Reaction_ID", "dbo.Reactions", "ID");
            AddForeignKey("dbo.Movies", "AddedBy_UserId", "dbo.UserProfile", "UserId");
            CreateIndex("dbo.Movies", "Reaction_ID");
            CreateIndex("dbo.Movies", "AddedBy_UserId");
            DropColumn("dbo.Movies", "ReleaseDate");
            DropColumn("dbo.Movies", "Genre");
            DropColumn("dbo.Movies", "Price");
            DropTable("dbo.ReactionReagents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReactionReagents",
                c => new
                    {
                        Reaction_ID = c.Int(nullable: false),
                        Reagent_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Reaction_ID, t.Reagent_ID });
            
            AddColumn("dbo.Movies", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Movies", "Genre", c => c.String());
            AddColumn("dbo.Movies", "ReleaseDate", c => c.DateTime(nullable: false));
            DropIndex("dbo.ReagentReactions", new[] { "Reaction_ID" });
            DropIndex("dbo.ReagentReactions", new[] { "Reagent_ID" });
            DropIndex("dbo.Movies", new[] { "AddedBy_UserId" });
            DropIndex("dbo.Movies", new[] { "Reaction_ID" });
            DropForeignKey("dbo.ReagentReactions", "Reaction_ID", "dbo.Reactions");
            DropForeignKey("dbo.ReagentReactions", "Reagent_ID", "dbo.Reagents");
            DropForeignKey("dbo.Movies", "AddedBy_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Movies", "Reaction_ID", "dbo.Reactions");
            DropColumn("dbo.Movies", "AddedBy_UserId");
            DropColumn("dbo.Movies", "Reaction_ID");
            DropColumn("dbo.Movies", "Url");
            DropColumn("dbo.Movies", "Desc");
            DropTable("dbo.ReagentReactions");
            CreateIndex("dbo.ReactionReagents", "Reagent_ID");
            CreateIndex("dbo.ReactionReagents", "Reaction_ID");
            AddForeignKey("dbo.ReactionReagents", "Reagent_ID", "dbo.Reagents", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ReactionReagents", "Reaction_ID", "dbo.Reactions", "ID", cascadeDelete: true);
        }
    }
}
