namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChatFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chats", "File", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chats", "File");
        }
    }
}
