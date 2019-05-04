namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileTaskRel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "FileId", "dbo.Files");
            DropIndex("dbo.Tasks", new[] { "FileId" });
            AddColumn("dbo.Files", "TaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.Files", "TaskId");
            AddForeignKey("dbo.Files", "TaskId", "dbo.Tasks", "Id", cascadeDelete: true);
            DropColumn("dbo.Tasks", "FileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "FileId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Files", "TaskId", "dbo.Tasks");
            DropIndex("dbo.Files", new[] { "TaskId" });
            DropColumn("dbo.Files", "TaskId");
            CreateIndex("dbo.Tasks", "FileId");
            AddForeignKey("dbo.Tasks", "FileId", "dbo.Files", "Id", cascadeDelete: true);
        }
    }
}
