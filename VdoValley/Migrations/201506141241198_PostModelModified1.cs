namespace VdoValley.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostModelModified1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "Post_PostId", c => c.Int());
            AddColumn("dbo.Posts", "Thumbnail", c => c.Binary());
            AddColumn("dbo.Posts", "ThumbnailUrl", c => c.String());
            AddColumn("dbo.Posts", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "TotalRating", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "RatingCount", c => c.Int(nullable: false));
            CreateIndex("dbo.Ratings", "Post_PostId");
            CreateIndex("dbo.Posts", "CategoryId");
            AddForeignKey("dbo.Posts", "CategoryId", "dbo.Categories", "CategoryId");
            AddForeignKey("dbo.Ratings", "Post_PostId", "dbo.Posts", "PostId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            DropIndex("dbo.Ratings", new[] { "Post_PostId" });
            DropColumn("dbo.Posts", "RatingCount");
            DropColumn("dbo.Posts", "TotalRating");
            DropColumn("dbo.Posts", "CategoryId");
            DropColumn("dbo.Posts", "ThumbnailUrl");
            DropColumn("dbo.Posts", "Thumbnail");
            DropColumn("dbo.Ratings", "Post_PostId");
        }
    }
}
