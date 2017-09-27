namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tookoutshoppingcartjoin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingCartJoinModels", "shoppingCart_Id", "dbo.ShoppingCartModels");
            DropForeignKey("dbo.ShoppingCartJoinModels", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ShoppingCartJoinModels", new[] { "shoppingCart_Id" });
            DropIndex("dbo.ShoppingCartJoinModels", new[] { "User_Id" });
            AddColumn("dbo.ShoppingCartModels", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ShoppingCartModels", "User_Id");
            AddForeignKey("dbo.ShoppingCartModels", "User_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.ShoppingCartJoinModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ShoppingCartJoinModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        shoppingCart_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.ShoppingCartModels", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ShoppingCartModels", new[] { "User_Id" });
            DropColumn("dbo.ShoppingCartModels", "User_Id");
            CreateIndex("dbo.ShoppingCartJoinModels", "User_Id");
            CreateIndex("dbo.ShoppingCartJoinModels", "shoppingCart_Id");
            AddForeignKey("dbo.ShoppingCartJoinModels", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ShoppingCartJoinModels", "shoppingCart_Id", "dbo.ShoppingCartModels", "Id");
        }
    }
}
