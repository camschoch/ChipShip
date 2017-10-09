namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingbooltoorderRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderRequests", "ShowOnDeliverer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderRequests", "ShowOnDeliverer");
        }
    }
}
