namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PosttId_PostId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "PosttId", "dbo.Posts");
            DropForeignKey("dbo.PostTags", "Post_PostId", "dbo.Posts");
            RenameColumn(table: "dbo.Ratings", name: "PosttId", newName: "PostId");
            RenameIndex(table: "dbo.Ratings", name: "IX_PosttId", newName: "IX_PostId");
            DropPrimaryKey("dbo.Posts");
            RenameColumn("dbo.Posts", "PosttId", "PostId");
            //AddColumn("dbo.Posts", "PostId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Posts", "PostId");
            AddForeignKey("dbo.Ratings", "PostId", "dbo.Posts", "PostId");
            AddForeignKey("dbo.PostTags", "Post_PostId", "dbo.Posts", "PostId", cascadeDelete: true);
            //DropColumn("dbo.Posts", "PosttId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "PosttId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.PostTags", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Ratings", "PostId", "dbo.Posts");
            DropPrimaryKey("dbo.Posts");
            DropColumn("dbo.Posts", "PostId");
            AddPrimaryKey("dbo.Posts", "PosttId");
            RenameIndex(table: "dbo.Ratings", name: "IX_PostId", newName: "IX_PosttId");
            RenameColumn(table: "dbo.Ratings", name: "PostId", newName: "PosttId");
            AddForeignKey("dbo.PostTags", "Post_PostId", "dbo.Posts", "PosttId", cascadeDelete: true);
            AddForeignKey("dbo.Ratings", "PosttId", "dbo.Posts", "PosttId");
        }
    }
}
