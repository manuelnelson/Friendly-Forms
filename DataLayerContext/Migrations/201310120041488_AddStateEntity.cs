namespace DataLayerContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddStateEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Abbreviation = c.String(maxLength: 5),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            Sql("insert into States values ('AL', 'Alabama');insert into States values ('AK', 'Alaska');insert into States values ('AZ', 'Arizona');insert into States values ('AR', 'Arkansas');insert into States values ('CA', 'California');insert into States values ('CO', 'Colorado');insert into States values ('CT', 'Connecticut');insert into States values ('DE', 'Delaware');insert into States values ('DC', 'District of Columbia');insert into States values ('FL', 'Florida');insert into States values ('GA', 'Georgia');insert into States values ('HI', 'Hawaii');insert into States values ('ID', 'Idaho');insert into States values ('IL', 'Illinois');insert into States values ('IN', 'Indiana');insert into States values ('IA', 'Iowa');insert into States values ('KS', 'Kansas');insert into States values ('KY', 'Kentucky');insert into States values ('LA', 'Louisiana');insert into States values ('ME', 'Maine');insert into States values ('MD', 'Maryland');insert into States values ('MA', 'Massachusetts');insert into States values ('MI', 'Michigan');insert into States values ('MN', 'Minnesota');insert into States values ('MS', 'Mississippi');insert into States values ('MO', 'Missouri');insert into States values ('MT', 'Montana');insert into States values ('NE', 'Nebraska');insert into States values ('NV', 'Nevada');insert into States values ('NH', 'New Hampshire');insert into States values ('NJ', 'New Jersey');insert into States values ('NM', 'New Mexico');insert into States values ('NY', 'New York');insert into States values ('NC', 'North Carolina');insert into States values ('ND', 'North Dakota');insert into States values ('OH', 'Ohio');insert into States values ('OK', 'Oklahoma');insert into States values ('OR', 'Oregon');insert into States values ('PA', 'Pennsylvania');insert into States values ('RI', 'Rhode Island');insert into States values ('SC', 'South Carolina');insert into States values ('SD', 'South Dakota');insert into States values ('TN', 'Tennessee');insert into States values ('TX', 'Texas');insert into States values ('UT', 'Utah');insert into States values ('VT', 'Vermont');insert into States values ('VA', 'Virginia');insert into States values ('WA', 'Washington');insert into States values ('WV', 'West Virginia');insert into States values ('WI', 'Wisconsin');insert into States values ('WY', 'Wyoming');");
        }

        public override void Down()
        {
            DropTable("dbo.States");
        }
    }
}
