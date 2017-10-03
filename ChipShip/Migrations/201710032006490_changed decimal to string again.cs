namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeddecimaltostringagain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "Lattitude", c => c.String());
            AlterColumn("dbo.Addresses", "Longitude", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "Longitude", c => c.Single(nullable: false));
            AlterColumn("dbo.Addresses", "Lattitude", c => c.Single(nullable: false));
        }
    }
}
