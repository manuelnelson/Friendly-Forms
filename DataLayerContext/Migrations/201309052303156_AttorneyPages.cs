namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttorneyPages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttorneyPages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LawFirmId = c.Long(nullable: false),
                        PageName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LawFirms", t => t.LawFirmId, cascadeDelete: true)
                .Index(t => t.LawFirmId);
            
            CreateTable(
                "dbo.AttorneyPageUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        AttorneyPageId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AttorneyPages", t => t.AttorneyPageId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AttorneyPageId);
            
            AddColumn("dbo.Users", "Position", c => c.String());
        }
        
        public override void Down()
        {
            DropIndex("dbo.AttorneyPageUsers", new[] { "AttorneyPageId" });
            DropIndex("dbo.AttorneyPageUsers", new[] { "UserId" });
            DropIndex("dbo.AttorneyPages", new[] { "LawFirmId" });
            DropForeignKey("dbo.AttorneyPageUsers", "AttorneyPageId", "dbo.AttorneyPages");
            DropForeignKey("dbo.AttorneyPageUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.AttorneyPages", "LawFirmId", "dbo.LawFirms");
            DropColumn("dbo.Users", "Position");
            DropTable("dbo.AttorneyPageUsers");
            DropTable("dbo.AttorneyPages");
        }
    }
}
