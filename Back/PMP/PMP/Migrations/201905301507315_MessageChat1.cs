namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageChat1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "File", c => c.String());
            AlterColumn("dbo.Messages", "Content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Content", c => c.String(nullable: false));
            DropColumn("dbo.Messages", "File");
        }
    }
}
