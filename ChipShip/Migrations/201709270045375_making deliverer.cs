namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makingdeliverer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ApplyToDeliverer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ApplyToDeliverer");
        }
    }
}
