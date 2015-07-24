namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ratingg_Rating : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Ratinggs", newName: "Ratings");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Ratings", newName: "Ratinggs");
        }
    }
}
