namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectTaskRelationDel1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "Project_Id" });
            DropColumn("dbo.Tasks", "Project_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "Project_Id", c => c.Int());
            CreateIndex("dbo.Tasks", "Project_Id");
            AddForeignKey("dbo.Tasks", "Project_Id", "dbo.Projects", "Id");
        }
    }
}
