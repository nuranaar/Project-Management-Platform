namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRelTaskFiles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "TaskId", "dbo.Tasks");
            DropIndex("dbo.Files", new[] { "TaskId" });
            AddColumn("dbo.Tasks", "FileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tasks", "FileId");
            AddForeignKey("dbo.Tasks", "FileId", "dbo.Files", "Id", cascadeDelete: false);
            DropColumn("dbo.Files", "TaskId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "TaskId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tasks", "FileId", "dbo.Files");
            DropIndex("dbo.Tasks", new[] { "FileId" });
            DropColumn("dbo.Tasks", "FileId");
            CreateIndex("dbo.Files", "TaskId");
            AddForeignKey("dbo.Files", "TaskId", "dbo.Tasks", "Id", cascadeDelete: false);
        }
    }
}
