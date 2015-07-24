namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VideoTypeeId_VideoTypeId_fk : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Videos", name: "VideoTypeeId", newName: "VideoTypeId");
            RenameIndex(table: "dbo.Videos", name: "IX_VideoTypeeId", newName: "IX_VideoTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Videos", name: "IX_VideoTypeId", newName: "IX_VideoTypeeId");
            RenameColumn(table: "dbo.Videos", name: "VideoTypeId", newName: "VideoTypeeId");
        }
    }
}
