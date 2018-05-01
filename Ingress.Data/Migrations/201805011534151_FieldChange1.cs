namespace Ingress.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldChange1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ModelAccess", "TimeTaken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ModelAccess", "TimeTaken", c => c.String(maxLength: 128));
        }
    }
}
