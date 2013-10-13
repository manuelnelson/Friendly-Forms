namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStateIdForeignKeyLogic : DbMigration
    {
        public override void Up()
        {
            Sql("Update PreexistingSupports Set StateId=11");
            AlterColumn("dbo.PreexistingSupports", "StateId", c => c.Long(nullable: false));
            AddForeignKey("dbo.PreexistingSupports", "StateId", "dbo.States", "Id", cascadeDelete: true);
            CreateIndex("dbo.PreexistingSupports", "StateId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PreexistingSupports", new[] { "StateId" });
            DropForeignKey("dbo.PreexistingSupports", "StateId", "dbo.States");
            AlterColumn("dbo.PreexistingSupports", "StateId", c => c.Long());
        }
    }
}
