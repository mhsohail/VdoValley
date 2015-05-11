namespace VdoValley.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "VideoType", c => c.String());
            AddColumn("dbo.Videos", "EmbedCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "EmbedCode");
            DropColumn("dbo.Videos", "VideoType");
        }
    }
}
