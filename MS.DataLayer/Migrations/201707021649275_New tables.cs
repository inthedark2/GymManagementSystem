namespace MS.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentDate = c.DateTime(nullable: false),
                        Client_Id = c.Int(),
                        SubscriptionType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.SubscriptionTypes", t => t.SubscriptionType_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.SubscriptionType_Id);
            
            CreateTable(
                "dbo.RecordForTrainings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrainingDate = c.DateTime(nullable: false),
                        Client_Id = c.Int(),
                        Trainer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Users", t => t.Trainer_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.Trainer_Id);
            
            AddColumn("dbo.Subscriptions", "StatusId", c => c.Int(nullable: false));
            AddColumn("dbo.Subscriptions", "FrozenFrom", c => c.DateTime(nullable: false));
            AddColumn("dbo.Subscriptions", "FrozenTo", c => c.DateTime(nullable: false));
            AddColumn("dbo.Subscriptions", "LeftFrozenDays", c => c.Int(nullable: false));
            AddColumn("dbo.SubscriptionTypes", "MaxFrozenDays", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecordForTrainings", "Trainer_Id", "dbo.Users");
            DropForeignKey("dbo.RecordForTrainings", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Payments", "SubscriptionType_Id", "dbo.SubscriptionTypes");
            DropForeignKey("dbo.Payments", "Client_Id", "dbo.Clients");
            DropIndex("dbo.RecordForTrainings", new[] { "Trainer_Id" });
            DropIndex("dbo.RecordForTrainings", new[] { "Client_Id" });
            DropIndex("dbo.Payments", new[] { "SubscriptionType_Id" });
            DropIndex("dbo.Payments", new[] { "Client_Id" });
            DropColumn("dbo.SubscriptionTypes", "MaxFrozenDays");
            DropColumn("dbo.Subscriptions", "LeftFrozenDays");
            DropColumn("dbo.Subscriptions", "FrozenTo");
            DropColumn("dbo.Subscriptions", "FrozenFrom");
            DropColumn("dbo.Subscriptions", "StatusId");
            DropTable("dbo.RecordForTrainings");
            DropTable("dbo.Payments");
        }
    }
}
