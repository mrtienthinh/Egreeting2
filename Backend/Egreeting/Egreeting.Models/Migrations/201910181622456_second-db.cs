namespace Egreeting.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seconddb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ecards", "EcardName", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ecards", "EcardName");
        }
    }
}
