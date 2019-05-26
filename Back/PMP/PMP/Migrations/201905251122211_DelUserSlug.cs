namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelUserSlug : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Position", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "Photo", c => c.String(maxLength: 150));
            AlterColumn("dbo.Users", "Biography", c => c.String(storeType: "ntext"));
            DropColumn("dbo.Users", "Slug");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Slug", c => c.String());
            AlterColumn("dbo.Users", "Biography", c => c.String(nullable: false, storeType: "ntext"));
            AlterColumn("dbo.Users", "Photo", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Users", "Position", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
