namespace DataLayerContext.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddCounties : DbMigration
    {
        public override void Up()
        {
            Sql(
                "insert into counties values ('Appling');  insert into counties values ('Atkinson');  insert into counties values ('Bacon');  insert into counties values ('Baker');  insert into counties values ('Baldwin');  insert into counties values ('Banks');  insert into counties values ('Barrow');  insert into counties values ('Bartow');  insert into counties values ('Ben Hill');  insert into counties values ('Berrien');  insert into counties values ('Bibb');  insert into counties values ('Bleckley');  insert into counties values ('Brantley');  insert into counties values ('Brooks');  insert into counties values ('Bryan');  insert into counties values ('Bulloch');  insert into counties values ('Burke');  insert into counties values ('Butts');  insert into counties values ('Calhoun');  insert into counties values ('Camden');  insert into counties values ('Candler');  insert into counties values ('Carroll');  insert into counties values ('Catoosa');  insert into counties values ('Charlton');  insert into counties values ('Chatham');  insert into counties values ('Chattahoochee');  insert into counties values ('Chattooga');  insert into counties values ('Cherokee');  insert into counties values ('Clarke');  insert into counties values ('Clay');  insert into counties values ('Clayton');  insert into counties values ('Clinch'); insert into counties values ('Coffee');  insert into counties values ('Colquitt');  insert into counties values ('Columbia');  insert into counties values ('Cook');  insert into counties values ('Coweta');  insert into counties values ('Crawford');  insert into counties values ('Crisp');  insert into counties values ('Dade');  insert into counties values ('Dawson');  insert into counties values ('Decatur');  insert into counties values ('Dodge');  insert into counties values ('Dooly');  insert into counties values ('Dougherty');  insert into counties values ('Douglas');  insert into counties values ('Early');  insert into counties values ('Echols');  insert into counties values ('Effingham');  insert into counties values ('Elbert');  insert into counties values ('Emanuel');  insert into counties values ('Evans');  insert into counties values ('Fannin');  insert into counties values ('Fayette');  insert into counties values ('Floyd');  insert into counties values ('Forsyth');  insert into counties values ('Franklin'); insert into counties values ('Gilmer');  insert into counties values ('Glascock');  insert into counties values ('Glynn');  insert into counties values ('Gordon');  insert into counties values ('Grady');  insert into counties values ('Greene'); insert into counties values ('Habersham');  insert into counties values ('Hall');  insert into counties values ('Hancock');  insert into counties values ('Haralson');  insert into counties values ('Harris');  insert into counties values ('Hart');  insert into counties values ('Heard');  insert into counties values ('Henry');  insert into counties values ('Houston');  insert into counties values ('Irwin');  insert into counties values ('Jackson');  insert into counties values ('Jasper');  insert into counties values ('Jeff Davis');  insert into counties values ('Jefferson');  insert into counties values ('Jenkins');  insert into counties values ('Johnson');  insert into counties values ('Jones');  insert into counties values ('Lamar');  insert into counties values ('Lanier');  insert into counties values ('Laurens');  insert into counties values ('Lee');  insert into counties values ('Liberty');  insert into counties values ('Lincoln');  insert into counties values ('Long');  insert into counties values ('Lowndes');  insert into counties values ('Lumpkin');  insert into counties values ('Macon');  insert into counties values ('Madison');  insert into counties values ('Marion');  insert into counties values ('McDuffie');  insert into counties values ('McIntosh');  insert into counties values ('Meriwether');  insert into counties values ('Miller');  insert into counties values ('Mitchell');  insert into counties values ('Monroe');  insert into counties values ('Montgomery');  insert into counties values ('Morgan');  insert into counties values ('Murray');  insert into counties values ('Muscogee');  insert into counties values ('Newton');  insert into counties values ('Oconee');  insert into counties values ('Oglethorpe');  insert into counties values ('Paulding');  insert into counties values ('Peach');  insert into counties values ('Pickens');  insert into counties values ('Pierce');  insert into counties values ('Pike');  insert into counties values ('Polk');  insert into counties values ('Pulaski');  insert into counties values ('Putnam');  insert into counties values ('Quitman');  insert into counties values ('Rabun');  insert into counties values ('Randolph');  insert into counties values ('Richmond');  insert into counties values ('Rockdale');  insert into counties values ('Schley');  insert into counties values ('Screven');  insert into counties values ('Seminole');  insert into counties values ('Spalding');  insert into counties values ('Stephens');  insert into counties values ('Stewart');  insert into counties values ('Sumter');  insert into counties values ('Talbot');  insert into counties values ('Taliaferro');  insert into counties values ('Tattnall');  insert into counties values ('Taylor');  insert into counties values ('Telfair');  insert into counties values ('Terrell');  insert into counties values ('Thomas');  insert into counties values ('Tift');  insert into counties values ('Toombs');  insert into counties values ('Towns');  insert into counties values ('Treutlen');  insert into counties values ('Troup');  insert into counties values ('Turner');  insert into counties values ('Twiggs');  insert into counties values ('Union');  insert into counties values ('Upson');  insert into counties values ('Walker');  insert into counties values ('Walton');  insert into counties values ('Ware');  insert into counties values ('Warren');  insert into counties values ('Washington');  insert into counties values ('Wayne');  insert into counties values ('Webster');  insert into counties values ('Wheeler');  insert into counties values ('White');  insert into counties values ('Whitfield');  insert into counties values ('Wilcox');  insert into counties values ('Wilkes');  insert into counties values ('Wilkinson');  insert into counties values ('Worth'); delete from counties where CountyName = 'Test'");
        }

        public override void Down()
        {
        }
    }
}