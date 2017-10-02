namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderstatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatusModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        status = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatusModels", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.StatusModels", new[] { "User_Id" });
            DropTable("dbo.StatusModels");
        }
    }
}
