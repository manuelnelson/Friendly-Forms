namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyNotifications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AttorneyClients", "ChangeNotification", c => c.Boolean(nullable: false));
            AddColumn("dbo.AttorneyClients", "PrintNotification", c => c.Boolean(nullable: false));
            DropColumn("dbo.AttorneyClients", "NotificationsEnabled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AttorneyClients", "NotificationsEnabled", c => c.Boolean(nullable: false));
            DropColumn("dbo.AttorneyClients", "PrintNotification");
            DropColumn("dbo.AttorneyClients", "ChangeNotification");
        }
    }
}
