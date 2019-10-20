namespace Egreeting.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seconddb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ecards", "EcardName", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ecards", "EcardName", c => c.String(maxLength: 150));
        }
    }
}
