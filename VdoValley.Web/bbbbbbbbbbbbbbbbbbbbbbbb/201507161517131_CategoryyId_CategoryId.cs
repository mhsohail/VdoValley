namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryyId_CategoryId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Postts", "CategoryyId", "dbo.Categories");
            DropForeignKey("dbo.Videos", "CategoryyId", "dbo.Categories");
            RenameColumn(table: "dbo.Postts", name: "CategoryyId", newName: "CategoryId");
            RenameIndex(table: "dbo.Postts", name: "IX_CategoryyId", newName: "IX_CategoryId");
            DropPrimaryKey("dbo.Categories");
            RenameColumn(table: "dbo.Categories", name: "CategoryyId", newName: "CategoryId");
            AddPrimaryKey("dbo.Categories", "CategoryId");
            AddForeignKey("dbo.Postts", "CategoryId", "dbo.Categories", "CategoryId");
            AddForeignKey("dbo.Videos", "CategoryyId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "CategoryyId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Videos", "CategoryyId", "dbo.Categories");
            DropForeignKey("dbo.Postts", "CategoryId", "dbo.Categories");
            DropPrimaryKey("dbo.Categories");
            DropColumn("dbo.Categories", "CategoryId");
            AddPrimaryKey("dbo.Categories", "CategoryyId");
            RenameIndex(table: "dbo.Postts", name: "IX_CategoryId", newName: "IX_CategoryyId");
            RenameColumn(table: "dbo.Postts", name: "CategoryId", newName: "CategoryyId");
            AddForeignKey("dbo.Videos", "CategoryyId", "dbo.Categories", "CategoryyId", cascadeDelete: true);
            AddForeignKey("dbo.Postts", "CategoryyId", "dbo.Categories", "CategoryyId");
        }
    }
}
