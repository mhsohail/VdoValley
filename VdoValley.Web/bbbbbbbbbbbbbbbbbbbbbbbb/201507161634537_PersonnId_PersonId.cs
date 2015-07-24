namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonnId_PersonId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.People");
            RenameColumn("dbo.People", "PersonnId", "PersonId");
            //AddColumn("dbo.People", "PersonId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.People", "PersonId");
            //DropColumn("dbo.People", "PersonnId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "PersonnId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.People");
            DropColumn("dbo.People", "PersonId");
            AddPrimaryKey("dbo.People", "PersonnId");
        }
    }
}
