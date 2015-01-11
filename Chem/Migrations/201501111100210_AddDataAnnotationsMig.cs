namespace Chem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataAnnotationsMig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Movies", "Desc", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Movies", "Url", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Reactions", "Desc", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Reagents", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reagents", "Name", c => c.String());
            AlterColumn("dbo.Reactions", "Desc", c => c.String());
            AlterColumn("dbo.Movies", "Url", c => c.String());
            AlterColumn("dbo.Movies", "Desc", c => c.String());
            AlterColumn("dbo.Movies", "Title", c => c.String());
        }
    }
}
