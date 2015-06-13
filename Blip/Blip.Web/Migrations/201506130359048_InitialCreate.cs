namespace Blip.Web.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                {
                    MessageID = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    DateTime = c.DateTime(nullable: false),
                    Body = c.String(),
                    SenderID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.Users", t => t.SenderID)
                .Index(t => t.SenderID);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserID = c.Int(nullable: false, identity: true),
                    UserName = c.String(),
                    Password = c.String(),
                    Role = c.String(),
                    Active = c.Boolean(nullable: false),
                    ActiveDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.UserID);

            CreateTable(
                "dbo.UserMessageReceived",
                c => new
                {
                    UserRefId = c.Int(nullable: false),
                    MessageRefId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.UserRefId, t.MessageRefId })
                .ForeignKey("dbo.Users", t => t.UserRefId, cascadeDelete: true)
                .ForeignKey("dbo.Messages", t => t.MessageRefId, cascadeDelete: true)
                .Index(t => t.UserRefId)
                .Index(t => t.MessageRefId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Messages", "SenderID", "dbo.Users");
            DropForeignKey("dbo.UserMessageReceived", "MessageRefId", "dbo.Messages");
            DropForeignKey("dbo.UserMessageReceived", "UserRefId", "dbo.Users");
            DropIndex("dbo.UserMessageReceived", new[] { "MessageRefId" });
            DropIndex("dbo.UserMessageReceived", new[] { "UserRefId" });
            DropIndex("dbo.Messages", new[] { "SenderID" });
            DropTable("dbo.UserMessageReceived");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
        }
    }
}