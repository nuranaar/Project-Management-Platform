namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTasmMembersTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskMembers", "Task_Id", "dbo.Tasks");
            DropIndex("dbo.TaskMembers", new[] { "Task_Id" });
            RenameColumn(table: "dbo.TaskMembers", name: "Task_Id", newName: "TaskId");
            AlterColumn("dbo.TaskMembers", "TaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.TaskMembers", "TaskId");
            AddForeignKey("dbo.TaskMembers", "TaskId", "dbo.Tasks", "Id", cascadeDelete: true);
            DropColumn("dbo.TaskMembers", "ProjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskMembers", "ProjectId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TaskMembers", "TaskId", "dbo.Tasks");
            DropIndex("dbo.TaskMembers", new[] { "TaskId" });
            AlterColumn("dbo.TaskMembers", "TaskId", c => c.Int());
            RenameColumn(table: "dbo.TaskMembers", name: "TaskId", newName: "Task_Id");
            CreateIndex("dbo.TaskMembers", "Task_Id");
            AddForeignKey("dbo.TaskMembers", "Task_Id", "dbo.Tasks", "Id");
        }
    }
}
