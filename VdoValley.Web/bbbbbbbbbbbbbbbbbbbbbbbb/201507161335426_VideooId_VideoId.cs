namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VideooId_VideoId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratinggs", "Post_VideooId", "dbo.Videos");
            DropForeignKey("dbo.Ratinggs", "VideooId", "dbo.Videos");
            DropForeignKey("dbo.VideoTag", "VideoId", "dbo.Videos");
            DropPrimaryKey("dbo.Videos");
            RenameColumn("dbo.Videos", "VideooId", "VideoId");
            RenameColumn(table: "dbo.Ratinggs", name: "Post_VideooId", newName: "Post_VideoId");
            RenameIndex(table: "dbo.Ratinggs", name: "IX_Post_VideooId", newName: "IX_Post_VideoId");
            AddPrimaryKey("dbo.Videos", "VideoId");
            AddForeignKey("dbo.Ratinggs", "Post_VideoId", "dbo.Videos", "VideoId");
            AddForeignKey("dbo.Ratinggs", "VideooId", "dbo.Videos", "VideoId", cascadeDelete: true);
            AddForeignKey("dbo.VideoTag", "VideoId", "dbo.Videos", "VideoId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.Videos", "VideooId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.VideoTag", "VideoId", "dbo.Videos");
            DropForeignKey("dbo.Ratinggs", "VideooId", "dbo.Videos");
            DropForeignKey("dbo.Ratinggs", "Post_VideoId", "dbo.Videos");
            DropPrimaryKey("dbo.Videos");
            DropColumn("dbo.Videos", "VideoId");
            AddPrimaryKey("dbo.Videos", "VideooId");
            RenameIndex(table: "dbo.Ratinggs", name: "IX_Post_VideoId", newName: "IX_Post_VideooId");
            RenameColumn(table: "dbo.Ratinggs", name: "Post_VideoId", newName: "Post_VideooId");
            AddForeignKey("dbo.VideoTag", "VideoId", "dbo.Videos", "VideooId", cascadeDelete: true);
            AddForeignKey("dbo.Ratinggs", "VideooId", "dbo.Videos", "VideooId", cascadeDelete: true);
            AddForeignKey("dbo.Ratinggs", "Post_VideooId", "dbo.Videos", "VideooId");
        }
    }
}
