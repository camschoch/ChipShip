namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingdberror : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCartModels", "salePrices", c => c.Single(nullable: false));
            DropColumn("dbo.ShoppingCartModels", "salePrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCartModels", "salePrice", c => c.Single(nullable: false));
            DropColumn("dbo.ShoppingCartModels", "salePrices");
        }
    }
}
