namespace MS.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editpayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "Subscription_Id", c => c.Int());
            CreateIndex("dbo.Payments", "Subscription_Id");
            AddForeignKey("dbo.Payments", "Subscription_Id", "dbo.Subscriptions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "Subscription_Id", "dbo.Subscriptions");
            DropIndex("dbo.Payments", new[] { "Subscription_Id" });
            DropColumn("dbo.Payments", "Subscription_Id");
        }
    }
}
