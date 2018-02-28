namespace AuthNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CurrentAmountfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Campaigns", "CurrentAmount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Campaigns", "CurrentAmount");
        }
    }
}
