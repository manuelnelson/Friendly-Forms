namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPaymentModifications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RecurringActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "AmountId", c => c.Int());
            AlterColumn("dbo.Users", "LawFirmId", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "LawFirmId", c => c.Int());
            DropColumn("dbo.Users", "AmountId");
            DropColumn("dbo.Users", "RecurringActive");
        }
    }
}
