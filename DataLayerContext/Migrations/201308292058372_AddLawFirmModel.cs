namespace DataLayerContext.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddLawFirmModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LawFirms",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Zip = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            //Since this field is nullable, we don't want a foreign key.
            AddColumn("dbo.Users", "LawFirmId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LawFirmId");
            DropTable("dbo.LawFirms");
        }
    }
}
