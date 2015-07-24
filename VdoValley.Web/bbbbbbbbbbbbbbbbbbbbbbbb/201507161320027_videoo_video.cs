namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videoo_video : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.Videoos", newName: "Videos");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Videos", newName: "Videoos");
        }
    }
}
