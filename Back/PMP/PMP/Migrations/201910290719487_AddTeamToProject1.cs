namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeamToProject1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "TeamId", "dbo.Teams");
            DropIndex("dbo.Projects", new[] { "TeamId" });
            AlterColumn("dbo.Projects", "TeamId", c => c.Int());
            CreateIndex("dbo.Projects", "TeamId");
            AddForeignKey("dbo.Projects", "TeamId", "dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "TeamId", "dbo.Teams");
            DropIndex("dbo.Projects", new[] { "TeamId" });
            AlterColumn("dbo.Projects", "TeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "TeamId");
            AddForeignKey("dbo.Projects", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
        }
    }
}
