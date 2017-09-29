namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednamestoshoppingcart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCartModels", "name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCartModels", "name");
        }
    }
}
