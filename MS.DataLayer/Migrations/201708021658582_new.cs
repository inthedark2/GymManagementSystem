namespace MS.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Subscriptions", name: "ClubCardId_Id", newName: "ClubCard_Id");
            RenameColumn(table: "dbo.Subscriptions", name: "SubscriptionTypeId_Id", newName: "SubscriptionType_Id");
            RenameColumn(table: "dbo.Users", name: "RoleId_Id", newName: "Role_Id");
            RenameIndex(table: "dbo.Subscriptions", name: "IX_ClubCardId_Id", newName: "IX_ClubCard_Id");
            RenameIndex(table: "dbo.Subscriptions", name: "IX_SubscriptionTypeId_Id", newName: "IX_SubscriptionType_Id");
            RenameIndex(table: "dbo.Users", name: "IX_RoleId_Id", newName: "IX_Role_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Users", name: "IX_Role_Id", newName: "IX_RoleId_Id");
            RenameIndex(table: "dbo.Subscriptions", name: "IX_SubscriptionType_Id", newName: "IX_SubscriptionTypeId_Id");
            RenameIndex(table: "dbo.Subscriptions", name: "IX_ClubCard_Id", newName: "IX_ClubCardId_Id");
            RenameColumn(table: "dbo.Users", name: "Role_Id", newName: "RoleId_Id");
            RenameColumn(table: "dbo.Subscriptions", name: "SubscriptionType_Id", newName: "SubscriptionTypeId_Id");
            RenameColumn(table: "dbo.Subscriptions", name: "ClubCard_Id", newName: "ClubCardId_Id");
        }
    }
}
