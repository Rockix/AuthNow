namespace AuthNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Donormodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Donors",
                c => new
                    {
                        DonorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.DonorId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Donors");
        }
    }
}
