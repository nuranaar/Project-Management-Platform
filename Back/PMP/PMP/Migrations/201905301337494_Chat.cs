namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Chat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        TeamId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        MyProperty = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chats", "UserId", "dbo.Users");
            DropForeignKey("dbo.Chats", "TeamId", "dbo.Teams");
            DropIndex("dbo.Chats", new[] { "UserId" });
            DropIndex("dbo.Chats", new[] { "TeamId" });
            DropTable("dbo.Chats");
        }
    }
}
