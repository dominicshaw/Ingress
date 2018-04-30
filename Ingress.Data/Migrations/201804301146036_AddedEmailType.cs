namespace Ingress.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmailType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BrokerEmail",
                c => new
                    {
                        ActivityID = c.Int(nullable: false),
                        Analyst = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.ActivityID)
                .ForeignKey("dbo.Activities", t => t.ActivityID)
                .Index(t => t.ActivityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BrokerEmail", "ActivityID", "dbo.Activities");
            DropIndex("dbo.BrokerEmail", new[] { "ActivityID" });
            DropTable("dbo.BrokerEmail");
        }
    }
}
