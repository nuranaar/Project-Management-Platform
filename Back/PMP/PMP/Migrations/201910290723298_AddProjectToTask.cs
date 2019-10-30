namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectToTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "ProjectId", c => c.Int());
            CreateIndex("dbo.Tasks", "ProjectId");
            AddForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropColumn("dbo.Tasks", "ProjectId");
        }
    }
}
