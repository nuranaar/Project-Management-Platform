namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeamToProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "TeamId", c => c.Int(nullable: false));
            AlterColumn("dbo.Notes", "Title", c => c.String(maxLength: 50));
            AlterColumn("dbo.Notes", "Desc", c => c.String(maxLength: 150));
            CreateIndex("dbo.Projects", "TeamId");
            AddForeignKey("dbo.Projects", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "TeamId", "dbo.Teams");
            DropIndex("dbo.Projects", new[] { "TeamId" });
            AlterColumn("dbo.Notes", "Desc", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Notes", "Title", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Projects", "TeamId");
        }
    }
}
