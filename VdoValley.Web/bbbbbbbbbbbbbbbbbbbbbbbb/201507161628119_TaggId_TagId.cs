namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaggId_TagId : DbMigration
    {
        public override void Up()
        {
            //DropIndex("dbo.Ratings", new[] { "Post_VideoId" });
            //DropColumn("dbo.Ratings", "PostId");
            //RenameColumn(table: "dbo.Ratings", name: "Post_VideoId", newName: "PostId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Ratings", name: "PostId", newName: "Post_VideoId");
            AddColumn("dbo.Ratings", "PostId", c => c.Int());
            CreateIndex("dbo.Ratings", "Post_VideoId");
        }
    }
}
