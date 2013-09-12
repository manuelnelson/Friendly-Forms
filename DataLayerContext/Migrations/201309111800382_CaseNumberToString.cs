namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaseNumberToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PreexistingSupports", "CaseNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PreexistingSupports", "CaseNumber", c => c.Int(nullable: false));
        }
    }
}
