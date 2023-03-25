namespace MS.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        SecondName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        Password = c.String(),
                        PasswordSalt = c.String(),
                        ValidationCode = c.Int(nullable: false),
                        GenderId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genders", t => t.GenderId_Id)
                .Index(t => t.GenderId_Id);
            
            CreateTable(
                "dbo.ClubCards",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ClubCardNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ClubCardId_Id = c.Int(),
                        SubscriptionTypeId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClubCards", t => t.ClubCardId_Id)
                .ForeignKey("dbo.SubscriptionTypes", t => t.SubscriptionTypeId_Id)
                .Index(t => t.ClubCardId_Id)
                .Index(t => t.SubscriptionTypeId_Id);
            
            CreateTable(
                "dbo.SubscriptionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        PasswordSalt = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        PhoneNumber = c.String(),
                        RoleId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId_Id)
                .Index(t => t.RoleId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId_Id", "dbo.Roles");
            DropForeignKey("dbo.Clients", "GenderId_Id", "dbo.Genders");
            DropForeignKey("dbo.Subscriptions", "SubscriptionTypeId_Id", "dbo.SubscriptionTypes");
            DropForeignKey("dbo.Subscriptions", "ClubCardId_Id", "dbo.ClubCards");
            DropForeignKey("dbo.ClubCards", "Id", "dbo.Clients");
            DropIndex("dbo.Users", new[] { "RoleId_Id" });
            DropIndex("dbo.Subscriptions", new[] { "SubscriptionTypeId_Id" });
            DropIndex("dbo.Subscriptions", new[] { "ClubCardId_Id" });
            DropIndex("dbo.ClubCards", new[] { "Id" });
            DropIndex("dbo.Clients", new[] { "GenderId_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Genders");
            DropTable("dbo.SubscriptionTypes");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.ClubCards");
            DropTable("dbo.Clients");
        }
    }
}
