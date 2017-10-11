namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changednameforpay : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ToBePaids", name: "Deliverer_Id", newName: "User_Id");
            RenameIndex(table: "dbo.ToBePaids", name: "IX_Deliverer_Id", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ToBePaids", name: "IX_User_Id", newName: "IX_Deliverer_Id");
            RenameColumn(table: "dbo.ToBePaids", name: "User_Id", newName: "Deliverer_Id");
        }
    }
}
