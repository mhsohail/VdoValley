namespace VdoValley.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class grgrgr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoTypes",
                c => new
                    {
                        VideoTypeId = c.Int(nullable: false, identity: true),
                        VideoTypeName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VideoTypeId);
            
            AddColumn("dbo.Videos", "VideoTypeId", c => c.Int(nullable: true));
            CreateIndex("dbo.Videos", "VideoTypeId");
            AddForeignKey("dbo.Videos", "VideoTypeId", "dbo.VideoTypes", "VideoTypeId", cascadeDelete: true);
            DropColumn("dbo.Videos", "VideoType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Videos", "VideoType", c => c.String());
            DropForeignKey("dbo.Videos", "VideoTypeId", "dbo.VideoTypes");
            DropIndex("dbo.Videos", new[] { "VideoTypeId" });
            DropColumn("dbo.Videos", "VideoTypeId");
            DropTable("dbo.VideoTypes");
        }
    }
}
