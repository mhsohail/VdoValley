namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutoImportedVideoCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AutoImportedVideos",
                c => new
                    {
                        AutoImportedVideoId = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                        IsShared = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AutoImportedVideoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AutoImportedVideos");
        }
    }
}
