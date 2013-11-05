namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserVerifiedToPaid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Paid", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "Verified");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Verified", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "Paid");
        }
    }
}
