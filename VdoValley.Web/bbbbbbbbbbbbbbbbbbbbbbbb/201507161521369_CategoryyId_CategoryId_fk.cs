namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryyId_CategoryId_fk : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Videos", name: "CategoryyId", newName: "CategoryId");
            RenameIndex(table: "dbo.Videos", name: "IX_CategoryyId", newName: "IX_CategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Videos", name: "IX_CategoryId", newName: "IX_CategoryyId");
            RenameColumn(table: "dbo.Videos", name: "CategoryId", newName: "CategoryyId");
        }
    }
}
