namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBogusField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courts", "BogusField");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courts", "BogusField", c => c.Int(nullable: false));
        }
    }
}
