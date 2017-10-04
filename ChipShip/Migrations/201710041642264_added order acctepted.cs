namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedorderacctepted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderRequests", "OrderAccepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderRequests", "OrderAccepted");
        }
    }
}
