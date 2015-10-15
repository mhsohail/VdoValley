namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFieldsAddedToAutoImportedVideoModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AutoImportedVideos", "Description", c => c.String());
            AddColumn("dbo.AutoImportedVideos", "ThumbnailURL", c => c.String());
            AddColumn("dbo.AutoImportedVideos", "EmbedId", c => c.String());
            AddColumn("dbo.AutoImportedVideos", "VideoId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AutoImportedVideos", "VideoId");
            DropColumn("dbo.AutoImportedVideos", "EmbedId");
            DropColumn("dbo.AutoImportedVideos", "ThumbnailURL");
            DropColumn("dbo.AutoImportedVideos", "Description");
        }
    }
}
