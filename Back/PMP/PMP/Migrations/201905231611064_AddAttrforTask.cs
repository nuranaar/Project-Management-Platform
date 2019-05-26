namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttrforTask : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Tasks", "Desc", c => c.String(nullable: false, storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "Desc", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Tasks", "Name", c => c.String());
        }
    }
}
