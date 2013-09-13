namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSecondaryAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Houses", "SecondaryAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Houses", "SecondaryAddress");
        }
    }
}
