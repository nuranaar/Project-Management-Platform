namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageFileDel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Messages", "File");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "File", c => c.String());
        }
    }
}
