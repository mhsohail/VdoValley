namespace VdoValley.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class grgrgrhhh : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Videos", new[] { "VideoTypeId" });
            AlterColumn("dbo.Videos", "VideoTypeId", c => c.Int());
            CreateIndex("dbo.Videos", "VideoTypeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Videos", new[] { "VideoTypeId" });
            AlterColumn("dbo.Videos", "VideoTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Videos", "VideoTypeId");
        }
    }
}
