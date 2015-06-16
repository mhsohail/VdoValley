namespace VdoValley.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostModelModified2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Ratings", name: "Post_PostId", newName: "PostId");
            RenameIndex(table: "dbo.Ratings", name: "IX_Post_PostId", newName: "IX_PostId");
            AddColumn("dbo.Ratings", "Post_VideoId", c => c.Int());
            CreateIndex("dbo.Ratings", "Post_VideoId");
            AddForeignKey("dbo.Ratings", "Post_VideoId", "dbo.Videos", "VideoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "Post_VideoId", "dbo.Videos");
            DropIndex("dbo.Ratings", new[] { "Post_VideoId" });
            DropColumn("dbo.Ratings", "Post_VideoId");
            RenameIndex(table: "dbo.Ratings", name: "IX_PostId", newName: "IX_Post_PostId");
            RenameColumn(table: "dbo.Ratings", name: "PostId", newName: "Post_PostId");
        }
    }
}
