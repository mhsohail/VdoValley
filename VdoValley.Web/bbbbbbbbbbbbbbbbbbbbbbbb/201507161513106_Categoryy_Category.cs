namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Categoryy_Category : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Categoryies", newName: "Categories");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Categories", newName: "Categoryies");
        }
    }
}
