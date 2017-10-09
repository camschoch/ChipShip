namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderRequests", "message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderRequests", "message");
        }
    }
}
