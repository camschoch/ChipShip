namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedorderpurchased : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderRequests", "OrderPurchased", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderRequests", "OrderPurchased");
        }
    }
}
