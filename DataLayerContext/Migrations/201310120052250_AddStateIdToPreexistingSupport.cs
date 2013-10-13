namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStateIdToPreexistingSupport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreexistingSupports", "StateId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PreexistingSupports", "StateId");
        }
    }
}
