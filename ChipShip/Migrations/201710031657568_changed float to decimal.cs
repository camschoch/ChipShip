namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedfloattodecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DelivererGeoLocationModels", "lat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DelivererGeoLocationModels", "lng", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DelivererGeoLocationModels", "lng", c => c.Single(nullable: false));
            AlterColumn("dbo.DelivererGeoLocationModels", "lat", c => c.Single(nullable: false));
        }
    }
}
