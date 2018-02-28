namespace AuthNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Transactionmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CampaignId = c.Int(nullable: false),
                        DonorId = c.Int(nullable: false),
                        PaymentOption = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Campaigns", t => t.CampaignId, cascadeDelete: true)
                .ForeignKey("dbo.Donors", t => t.DonorId, cascadeDelete: true)
                .Index(t => t.CampaignId)
                .Index(t => t.DonorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "DonorId", "dbo.Donors");
            DropForeignKey("dbo.Transactions", "CampaignId", "dbo.Campaigns");
            DropIndex("dbo.Transactions", new[] { "DonorId" });
            DropIndex("dbo.Transactions", new[] { "CampaignId" });
            DropTable("dbo.Transactions");
        }
    }
}
