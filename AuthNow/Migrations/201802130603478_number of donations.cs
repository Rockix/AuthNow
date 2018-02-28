namespace AuthNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numberofdonations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Donors", "NumberOfDonations", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Donors", "NumberOfDonations");
        }
    }
}
