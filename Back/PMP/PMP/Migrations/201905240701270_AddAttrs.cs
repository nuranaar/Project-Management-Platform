namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttrs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "Desc", c => c.String(nullable: false, storeType: "ntext"));
            AlterColumn("dbo.Files", "Name", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Files", "Weight", c => c.String(nullable: false));
            AlterColumn("dbo.Files", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Tasks", "Desc", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Tasks", "Slug", c => c.String(nullable: false));
            AlterColumn("dbo.Checklists", "Text", c => c.String(nullable: false, storeType: "ntext"));
            AlterColumn("dbo.Notes", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Notes", "Desc", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "Desc", c => c.String());
            AlterColumn("dbo.Notes", "Title", c => c.String());
            AlterColumn("dbo.Checklists", "Text", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Tasks", "Slug", c => c.String());
            AlterColumn("dbo.Tasks", "Desc", c => c.String(nullable: false, storeType: "ntext"));
            AlterColumn("dbo.Files", "Type", c => c.String());
            AlterColumn("dbo.Files", "Weight", c => c.String());
            AlterColumn("dbo.Files", "Name", c => c.String());
            AlterColumn("dbo.Activities", "Desc", c => c.String(storeType: "ntext"));
        }
    }
}
