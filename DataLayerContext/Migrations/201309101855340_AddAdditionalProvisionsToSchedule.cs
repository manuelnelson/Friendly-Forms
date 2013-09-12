namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdditionalProvisionsToSchedule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "AnyAdditionalProvisions", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schedules", "AnyAdditionalProvisions");
        }
    }
}
