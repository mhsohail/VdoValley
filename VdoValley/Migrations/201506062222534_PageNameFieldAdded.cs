namespace VdoValley.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PageNameFieldAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "PageName", c => c.String());
            AddColumn("dbo.FbVideos", "IsAddedToVdoValley", c => c.Boolean(nullable: false));
            AddColumn("dbo.FbVideos", "PageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FbVideos", "PageName");
            DropColumn("dbo.FbVideos", "IsAddedToVdoValley");
            DropColumn("dbo.Videos", "PageName");
        }
    }
}
