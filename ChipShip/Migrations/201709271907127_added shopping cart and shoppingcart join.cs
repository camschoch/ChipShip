namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedshoppingcartandshoppingcartjoin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCartModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        itemId = c.Int(nullable: false),
                        amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShoppingCartJoinModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        shoppingCart_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ShoppingCartModels", t => t.shoppingCart_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.shoppingCart_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCartJoinModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ShoppingCartJoinModels", "shoppingCart_Id", "dbo.ShoppingCartModels");
            DropIndex("dbo.ShoppingCartJoinModels", new[] { "User_Id" });
            DropIndex("dbo.ShoppingCartJoinModels", new[] { "shoppingCart_Id" });
            DropTable("dbo.ShoppingCartJoinModels");
            DropTable("dbo.ShoppingCartModels");
        }
    }
}
