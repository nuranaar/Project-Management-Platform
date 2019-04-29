namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskStages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskStage = c.String(name: "Task Stage"),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tasks", "TaskStageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tasks", "TaskStageId");
            AddForeignKey("dbo.Tasks", "TaskStageId", "dbo.TaskStages", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "TaskStageId", "dbo.TaskStages");
            DropIndex("dbo.Tasks", new[] { "TaskStageId" });
            DropColumn("dbo.Tasks", "TaskStageId");
            DropTable("dbo.TaskStages");
        }
    }
}
