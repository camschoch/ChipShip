namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeddecimaltostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DelivererGeoLocationModels", "lat", c => c.String());
            AlterColumn("dbo.DelivererGeoLocationModels", "lng", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DelivererGeoLocationModels", "lng", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DelivererGeoLocationModels", "lat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
