namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelTeamDesc : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Teams", "Desc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "Desc", c => c.String(nullable: false, storeType: "ntext"));
        }
    }
}
