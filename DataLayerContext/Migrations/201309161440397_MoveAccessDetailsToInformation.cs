namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveAccessDetailsToInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Information", "AccessOfRightsDetails", c => c.String());
            DropColumn("dbo.Communications", "AccessOfRights");
            DropColumn("dbo.Communications", "AccessOfRightsDetails");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Communications", "AccessOfRightsDetails", c => c.String());
            AddColumn("dbo.Communications", "AccessOfRights", c => c.Int(nullable: false));
            DropColumn("dbo.Information", "AccessOfRightsDetails");
        }
    }
}
