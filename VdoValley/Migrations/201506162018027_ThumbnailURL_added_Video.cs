namespace VdoValley.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThumbnailURL_added_Video : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "Thumbnail", c => c.String());
            AddColumn("dbo.Videos", "ThumbnailURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "ThumbnailURL");
            DropColumn("dbo.Videos", "Thumbnail");
        }
    }
}
