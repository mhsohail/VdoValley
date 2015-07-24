namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VideoTypeeId_VideoTypeId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Videos", "VideoTypeeId", "dbo.VideoTypes");
            DropPrimaryKey("dbo.VideoTypes");
            //AddColumn("dbo.VideoTypes", "VideoTypeId", c => c.Int(nullable: false, identity: true));
            RenameColumn("dbo.VideoTypes", "VideoTypeeId", "VideoTypeId");
            AddPrimaryKey("dbo.VideoTypes", "VideoTypeId");
            AddForeignKey("dbo.Videos", "VideoTypeeId", "dbo.VideoTypes", "VideoTypeId", cascadeDelete: true);
            //DropColumn("dbo.VideoTypes", "VideoTypeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VideoTypes", "VideoTypeeId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Videos", "VideoTypeeId", "dbo.VideoTypes");
            DropPrimaryKey("dbo.VideoTypes");
            DropColumn("dbo.VideoTypes", "VideoTypeId");
            AddPrimaryKey("dbo.VideoTypes", "VideoTypeeId");
            AddForeignKey("dbo.Videos", "VideoTypeeId", "dbo.VideoTypes", "VideoTypeeId", cascadeDelete: true);
        }
    }
}
