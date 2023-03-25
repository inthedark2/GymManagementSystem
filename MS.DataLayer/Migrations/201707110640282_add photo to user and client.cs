namespace MS.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addphototouserandclient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "PhotoPath", c => c.String());
            AddColumn("dbo.RecordForTrainings", "StatusId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "PhotoPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PhotoPath");
            DropColumn("dbo.RecordForTrainings", "StatusId");
            DropColumn("dbo.Clients", "PhotoPath");
        }
    }
}
