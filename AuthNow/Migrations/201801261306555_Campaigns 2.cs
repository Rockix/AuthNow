namespace AuthNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campaigns2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        CampaignId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Target = c.Decimal(nullable: false, precision: 18, scale: 2),
                        City = c.String(),
                        Country = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CampaignId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Campaigns", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Campaigns", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Campaigns", new[] { "User_Id" });
            DropIndex("dbo.Campaigns", new[] { "CategoryId" });
            DropTable("dbo.Campaigns");
        }
    }
}
