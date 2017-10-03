namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddeliverertoorderrequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderRequests", "Deliverer_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.OrderRequests", "Deliverer_Id");
            AddForeignKey("dbo.OrderRequests", "Deliverer_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderRequests", "Deliverer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.OrderRequests", new[] { "Deliverer_Id" });
            DropColumn("dbo.OrderRequests", "Deliverer_Id");
        }
    }
}
