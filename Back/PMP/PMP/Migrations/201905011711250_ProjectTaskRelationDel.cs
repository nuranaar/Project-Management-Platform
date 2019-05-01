namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectTaskRelationDel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            RenameColumn(table: "dbo.Tasks", name: "ProjectId", newName: "Project_Id");
            AlterColumn("dbo.Tasks", "Project_Id", c => c.Int());
            CreateIndex("dbo.Tasks", "Project_Id");
            AddForeignKey("dbo.Tasks", "Project_Id", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "Project_Id" });
            AlterColumn("dbo.Tasks", "Project_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Tasks", name: "Project_Id", newName: "ProjectId");
            CreateIndex("dbo.Tasks", "ProjectId");
            AddForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
    }
}
