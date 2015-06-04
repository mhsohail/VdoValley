namespace VdoValley.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmbedIdFieldAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FbVideos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedTime = c.String(),
                        Description = c.String(),
                        EmbedHtml = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Videos", "EmbedId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "EmbedId");
            DropTable("dbo.FbVideos");
        }
    }
}
