namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FbVideoo_FbVideo : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FbVideoos", newName: "FbVideos");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.FbVideos", newName: "FbVideoos");
        }
    }
}
