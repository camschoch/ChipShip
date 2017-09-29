namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsalePricetoshoppingcart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCartModels", "salePrice", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCartModels", "salePrice");
        }
    }
}
