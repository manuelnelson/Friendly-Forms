namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttorneyClientAndUserIdToAttorney : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AttorneyPageUsers", "UserId", "dbo.Users");
            DropIndex("dbo.AttorneyPageUsers", new[] { "UserId" });
            CreateTable(
                "dbo.AttorneyClients",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ClientUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.AttorneyPages", "UserId", c => c.Long(nullable: false));
            AddForeignKey("dbo.AttorneyPages", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AttorneyPageUsers", "UserId", "dbo.Users", "Id");
            CreateIndex("dbo.AttorneyPages", "UserId");
            CreateIndex("dbo.AttorneyPageUsers", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AttorneyClients", new[] { "UserId" });
            DropIndex("dbo.AttorneyPageUsers", new[] { "UserId" });
            DropIndex("dbo.AttorneyPages", new[] { "UserId" });
            DropForeignKey("dbo.AttorneyClients", "UserId", "dbo.Users");
            DropForeignKey("dbo.AttorneyPageUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.AttorneyPages", "UserId", "dbo.Users");
            DropColumn("dbo.AttorneyPages", "UserId");
            DropTable("dbo.AttorneyClients");
            CreateIndex("dbo.AttorneyPageUsers", "UserId");
            AddForeignKey("dbo.AttorneyPageUsers", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
