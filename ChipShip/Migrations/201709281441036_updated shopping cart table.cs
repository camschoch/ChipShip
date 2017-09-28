namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedshoppingcarttable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingCartModels", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ShoppingCartModels", new[] { "User_Id" });
            DropColumn("dbo.ShoppingCartModels", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCartModels", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ShoppingCartModels", "User_Id");
            AddForeignKey("dbo.ShoppingCartModels", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
