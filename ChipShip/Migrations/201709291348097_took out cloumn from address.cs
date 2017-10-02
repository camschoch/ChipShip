namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tookoutcloumnfromaddress : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Addresses", "Zone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "Zone", c => c.String());
        }
    }
}
