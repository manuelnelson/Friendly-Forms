namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaidSubscriptionField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LawFirms", "Subscription", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LawFirms", "Subscription");
        }
    }
}
