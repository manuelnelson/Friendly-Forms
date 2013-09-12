namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsJointCustody : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Participants", "IsJointCustody", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Participants", "IsJointCustody");
        }
    }
}
