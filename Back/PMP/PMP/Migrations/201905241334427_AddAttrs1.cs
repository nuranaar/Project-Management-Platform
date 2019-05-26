namespace PMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttrs1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Projects", name: "Project Admin", newName: "UserId");
            RenameIndex(table: "dbo.Projects", name: "IX_Project Admin", newName: "IX_UserId");
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Surname", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "Position", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Photo", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Users", "Biography", c => c.String(nullable: false, storeType: "ntext"));
            AlterColumn("dbo.Tasks", "Slug", c => c.String());
            AlterColumn("dbo.TaskStages", "Task Stage", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Teams", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Teams", "Desc", c => c.String(nullable: false, storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teams", "Desc", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Teams", "Name", c => c.String());
            AlterColumn("dbo.Projects", "Name", c => c.String());
            AlterColumn("dbo.TaskStages", "Task Stage", c => c.String());
            AlterColumn("dbo.Tasks", "Slug", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Biography", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Users", "Photo", c => c.String());
            AlterColumn("dbo.Users", "Position", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Surname", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            RenameIndex(table: "dbo.Projects", name: "IX_UserId", newName: "IX_Project Admin");
            RenameColumn(table: "dbo.Projects", name: "UserId", newName: "Project Admin");
        }
    }
}
