namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VideoTypee_VideoType : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.VideoTypees", newName: "VideoTypes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.VideoTypes", newName: "VideoTypees");
        }
    }
}
