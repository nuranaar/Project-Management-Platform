namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttrsUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Photo", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Photo", c => c.String(maxLength: 150));
        }
    }
}
