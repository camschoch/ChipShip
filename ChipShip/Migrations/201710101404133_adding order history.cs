namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingorderhistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCartModels", "Orders_Id", c => c.Int());
            CreateIndex("dbo.ShoppingCartModels", "Orders_Id");
            AddForeignKey("dbo.ShoppingCartModels", "Orders_Id", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCartModels", "Orders_Id", "dbo.Orders");
            DropIndex("dbo.ShoppingCartModels", new[] { "Orders_Id" });
            DropColumn("dbo.ShoppingCartModels", "Orders_Id");
        }
    }
}
