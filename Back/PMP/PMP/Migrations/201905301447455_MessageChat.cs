namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageChat : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Chats", "UserId", "dbo.Users");
            DropIndex("dbo.Chats", new[] { "UserId" });
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        ChatId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.ChatId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ChatId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Chats", "Photo", c => c.String());
            DropColumn("dbo.Chats", "Message");
            DropColumn("dbo.Chats", "UserId");
            DropColumn("dbo.Chats", "MyProperty");
            DropColumn("dbo.Chats", "File");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Chats", "File", c => c.String());
            AddColumn("dbo.Chats", "MyProperty", c => c.DateTime(nullable: false));
            AddColumn("dbo.Chats", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Chats", "Message", c => c.String());
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropColumn("dbo.Chats", "Photo");
            DropTable("dbo.Messages");
            CreateIndex("dbo.Chats", "UserId");
            AddForeignKey("dbo.Chats", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
