namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Postt_Post : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Postts", newName: "Posts");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Posts", newName: "Postts");
        }
    }
}
