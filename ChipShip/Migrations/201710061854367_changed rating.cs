namespace ChipShip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedrating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "raiting", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "raitingCount", c => c.Int(nullable: false));
            DropColumn("dbo.Ratings", "twoStar");
            DropColumn("dbo.Ratings", "threeStar");
            DropColumn("dbo.Ratings", "fourStar");
            DropColumn("dbo.Ratings", "fiveStar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "fiveStar", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "fourStar", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "threeStar", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "twoStar", c => c.Int(nullable: false));
            DropColumn("dbo.Ratings", "raitingCount");
            DropColumn("dbo.Ratings", "raiting");
        }
    }
}
