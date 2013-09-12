namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BogusField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courts", "BogusField", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courts", "BogusField");
        }
    }
}
