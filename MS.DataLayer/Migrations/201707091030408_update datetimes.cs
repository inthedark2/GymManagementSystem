namespace MS.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatetimes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subscriptions", "FrozenFrom", c => c.DateTime());
            AlterColumn("dbo.Subscriptions", "FrozenTo", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscriptions", "FrozenTo", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Subscriptions", "FrozenFrom", c => c.DateTime(nullable: false));
        }
    }
}
