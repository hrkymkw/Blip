namespace Blip.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserNameUniqueIndex : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Users", "UserName", unique: true, name: "UX_UserName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", "UX_UserName");
            AlterColumn("dbo.Users", "UserName", c => c.String());
        }
    }
}
