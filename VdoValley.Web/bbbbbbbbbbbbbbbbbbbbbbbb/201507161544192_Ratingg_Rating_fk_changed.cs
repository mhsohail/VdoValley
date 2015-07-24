namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ratingg_Rating_fk_changed : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Ratings", name: "VideooId", newName: "VideoId");
            RenameIndex(table: "dbo.Ratings", name: "IX_VideooId", newName: "IX_VideoId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Ratings", name: "IX_VideoId", newName: "IX_VideooId");
            RenameColumn(table: "dbo.Ratings", name: "VideoId", newName: "VideooId");
        }
    }
}
