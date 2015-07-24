namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tagg_Tag : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Taggs", newName: "Tags");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Tags", newName: "Taggs");
        }
    }
}
