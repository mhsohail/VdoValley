namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TitleFieldAddedToAutoImportedVideoModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutoImportedVideos", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AutoImportedVideos", "Title");
        }
    }
}
