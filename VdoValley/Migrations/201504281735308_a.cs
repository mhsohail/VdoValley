namespace VdoValley.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        VideoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VideoTag",
                c => new
                    {
                        VideoRefId = c.Int(nullable: false),
                        TagRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VideoRefId, t.TagRefId })
                .ForeignKey("dbo.Videos", t => t.VideoRefId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagRefId, cascadeDelete: true)
                .Index(t => t.VideoRefId)
                .Index(t => t.TagRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoTag", "TagRefId", "dbo.Tags");
            DropForeignKey("dbo.VideoTag", "VideoRefId", "dbo.Videos");
            DropIndex("dbo.VideoTag", new[] { "TagRefId" });
            DropIndex("dbo.VideoTag", new[] { "VideoRefId" });
            DropTable("dbo.VideoTag");
            DropTable("dbo.Tags");
        }
    }
}
