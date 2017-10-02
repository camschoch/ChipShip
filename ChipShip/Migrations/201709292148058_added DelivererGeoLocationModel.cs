namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDelivererGeoLocationModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DelivererGeoLocationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        lat = c.Single(nullable: false),
                        lng = c.Single(nullable: false),
                        tracking = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DelivererGeoLocationModels", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.DelivererGeoLocationModels", new[] { "User_Id" });
            DropTable("dbo.DelivererGeoLocationModels");
        }
    }
}
