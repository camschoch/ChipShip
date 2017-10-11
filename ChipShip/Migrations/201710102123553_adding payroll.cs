namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingpayroll : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToBePaids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        amount = c.Double(nullable: false),
                        paid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ToBePaids");
        }
    }
}
