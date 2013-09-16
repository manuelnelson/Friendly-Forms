namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnyAdditionalToInt : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE dbo.Schedules DROP CONSTRAINT DF__Schedules__AnyAd__43D61337");
            AlterColumn("dbo.Schedules", "AnyAdditionalProvisions", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Schedules", "AnyAdditionalProvisions", c => c.Boolean(nullable: false));
        }
    }
}
