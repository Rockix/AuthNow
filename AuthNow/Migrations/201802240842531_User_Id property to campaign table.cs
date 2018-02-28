namespace AuthNow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_Idpropertytocampaigntable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Campaigns", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Campaigns", new[] { "User_Id" });
            AddColumn("dbo.Campaigns", "User_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.Campaigns", "User_Id", c => c.String());
            CreateIndex("dbo.Campaigns", "User_Id1");
            AddForeignKey("dbo.Campaigns", "User_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Campaigns", "User_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.Campaigns", new[] { "User_Id1" });
            AlterColumn("dbo.Campaigns", "User_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Campaigns", "User_Id1");
            CreateIndex("dbo.Campaigns", "User_Id");
            AddForeignKey("dbo.Campaigns", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
