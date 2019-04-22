namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Desc = c.String(storeType: "ntext"),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Position = c.String(),
                        Photo = c.String(),
                        Biography = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        Name = c.String(),
                        Weight = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Name = c.String(),
                        Desc = c.String(storeType: "ntext"),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Checklists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(storeType: "ntext"),
                        Checked = c.Boolean(nullable: false),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: false)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Desc = c.String(),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: false)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectAdmin = c.Int(name: "Project Admin", nullable: false),
                        Name = c.String(),
                        Desc = c.String(storeType: "ntext"),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ProjectAdmin, cascadeDelete: false)
                .Index(t => t.ProjectAdmin);
            
            CreateTable(
                "dbo.ProjectMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TaskMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Task_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.Task_Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.Task_Id);
            
            CreateTable(
                "dbo.TeamMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.TeamId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamAdmin = c.Int(name: "Team Admin", nullable: false),
                        Name = c.String(),
                        Desc = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.TeamAdmin, cascadeDelete: false)
                .Index(t => t.TeamAdmin);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "Team Admin", "dbo.Users");
            DropForeignKey("dbo.TeamMembers", "UserId", "dbo.Users");
            DropForeignKey("dbo.TeamMembers", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Files", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tasks", "UserId", "dbo.Users");
            DropForeignKey("dbo.TaskMembers", "UserId", "dbo.Users");
            DropForeignKey("dbo.TaskMembers", "Task_Id", "dbo.Tasks");
            DropForeignKey("dbo.Projects", "Project Admin", "dbo.Users");
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectMembers", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProjectMembers", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Notes", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Files", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Checklists", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Activities", "UserId", "dbo.Users");
            DropIndex("dbo.Teams", new[] { "Team Admin" });
            DropIndex("dbo.TeamMembers", new[] { "UserId" });
            DropIndex("dbo.TeamMembers", new[] { "TeamId" });
            DropIndex("dbo.TaskMembers", new[] { "Task_Id" });
            DropIndex("dbo.TaskMembers", new[] { "UserId" });
            DropIndex("dbo.ProjectMembers", new[] { "UserId" });
            DropIndex("dbo.ProjectMembers", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "Project Admin" });
            DropIndex("dbo.Notes", new[] { "TaskId" });
            DropIndex("dbo.Checklists", new[] { "TaskId" });
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropIndex("dbo.Tasks", new[] { "UserId" });
            DropIndex("dbo.Files", new[] { "TaskId" });
            DropIndex("dbo.Files", new[] { "UserId" });
            DropIndex("dbo.Activities", new[] { "UserId" });
            DropTable("dbo.Teams");
            DropTable("dbo.TeamMembers");
            DropTable("dbo.TaskMembers");
            DropTable("dbo.ProjectMembers");
            DropTable("dbo.Projects");
            DropTable("dbo.Notes");
            DropTable("dbo.Checklists");
            DropTable("dbo.Tasks");
            DropTable("dbo.Files");
            DropTable("dbo.Users");
            DropTable("dbo.Activities");
        }
    }
}
