namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSlug : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Slug", c => c.String());
            AddColumn("dbo.Tasks", "Slug", c => c.String());
            AddColumn("dbo.Projects", "Slug", c => c.String());
            AddColumn("dbo.Teams", "Slug", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "Slug");
            DropColumn("dbo.Projects", "Slug");
            DropColumn("dbo.Tasks", "Slug");
            DropColumn("dbo.Users", "Slug");
        }
    }
}
