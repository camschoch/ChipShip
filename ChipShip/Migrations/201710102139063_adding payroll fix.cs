namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingpayrollfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToBePaids", "Deliverer_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ToBePaids", "Deliverer_Id");
            AddForeignKey("dbo.ToBePaids", "Deliverer_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToBePaids", "Deliverer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ToBePaids", new[] { "Deliverer_Id" });
            DropColumn("dbo.ToBePaids", "Deliverer_Id");
        }
    }
}
