namespace Ingress.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class NewDataTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelAccess",
                c => new
                    {
                        ActivityID = c.Int(nullable: false),
                        TimeTaken = c.String(maxLength: 128),
                        Analyst = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.ActivityID)
                .ForeignKey("dbo.Activities", t => t.ActivityID)
                .Index(t => t.ActivityID);
            
            CreateTable(
                "dbo.PhoneCall",
                c => new
                    {
                        ActivityID = c.Int(nullable: false),
                        TimeTaken = c.String(maxLength: 128),
                        Analyst = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.ActivityID)
                .ForeignKey("dbo.Activities", t => t.ActivityID)
                .Index(t => t.ActivityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhoneCall", "ActivityID", "dbo.Activities");
            DropForeignKey("dbo.ModelAccess", "ActivityID", "dbo.Activities");
            DropIndex("dbo.PhoneCall", new[] { "ActivityID" });
            DropIndex("dbo.ModelAccess", new[] { "ActivityID" });
            DropTable("dbo.PhoneCall");
            DropTable("dbo.ModelAccess");
        }
    }
}
