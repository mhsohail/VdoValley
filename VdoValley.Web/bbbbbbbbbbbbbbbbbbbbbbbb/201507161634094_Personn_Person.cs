namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Personn_Person : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Personns", newName: "People");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.People", newName: "Personns");
        }
    }
}
