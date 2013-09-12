namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssetsFieldAdded : DbMigration
    {
        public override void Up()
        {
            Sql("Update Assets Set AdditionalAssets = '0'");
            AddColumn("dbo.Assets", "AdditionalAssetsDescription", c => c.String());
            AlterColumn("dbo.Assets", "AdditionalAssets", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Assets", "AdditionalAssets", c => c.String());
            DropColumn("dbo.Assets", "AdditionalAssetsDescription");
        }
    }
}
