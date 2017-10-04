namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedratingtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        twoStar = c.Int(nullable: false),
                        threeStar = c.Int(nullable: false),
                        fourStar = c.Int(nullable: false),
                        fiveStar = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Ratings", new[] { "User_Id" });
            DropTable("dbo.Ratings");
        }
    }
}
