namespace MS.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesubscription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subscriptions", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Subscriptions", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscriptions", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Subscriptions", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
