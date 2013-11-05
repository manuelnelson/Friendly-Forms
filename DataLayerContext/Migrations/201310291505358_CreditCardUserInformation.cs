namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreditCardUserInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RecurringDateStart", c => c.DateTime());
            AddColumn("dbo.Users", "CcInfoKey", c => c.String());
            AddColumn("dbo.Users", "CustomerKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CustomerKey");
            DropColumn("dbo.Users", "CcInfoKey");
            DropColumn("dbo.Users", "RecurringDateStart");
        }
    }
}
