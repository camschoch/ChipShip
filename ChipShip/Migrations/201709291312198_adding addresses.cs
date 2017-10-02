namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingaddresses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        addressLine = c.String(),
                        Lattitude = c.Single(nullable: false),
                        Longitude = c.Single(nullable: false),
                        Zone = c.String(),
                        City_ID = c.Int(),
                        Zip_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cities", t => t.City_ID)
                .ForeignKey("dbo.ZipCodes", t => t.Zip_ID)
                .Index(t => t.City_ID)
                .Index(t => t.Zip_ID);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        State_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.States", t => t.State_ID)
                .Index(t => t.State_ID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        State = c.String(),
                        abbreviation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ZipCodes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        zip = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Address_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Addresses", t => t.Address_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Address_ID)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAddresses", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserAddresses", "Address_ID", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "Zip_ID", "dbo.ZipCodes");
            DropForeignKey("dbo.Addresses", "City_ID", "dbo.Cities");
            DropForeignKey("dbo.Cities", "State_ID", "dbo.States");
            DropIndex("dbo.UserAddresses", new[] { "User_Id" });
            DropIndex("dbo.UserAddresses", new[] { "Address_ID" });
            DropIndex("dbo.Cities", new[] { "State_ID" });
            DropIndex("dbo.Addresses", new[] { "Zip_ID" });
            DropIndex("dbo.Addresses", new[] { "City_ID" });
            DropTable("dbo.UserAddresses");
            DropTable("dbo.ZipCodes");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Addresses");
        }
    }
}
