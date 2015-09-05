namespace VdoValley.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        VideoId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Url = c.String(),
                        VideoTypeId = c.Int(),
                        EmbedCode = c.String(),
                        EmbedId = c.String(),
                        Description = c.String(),
                        Thumbnail = c.String(),
                        ThumbnailURL = c.String(),
                        thumbnail_large_url = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Featured = c.Boolean(nullable: false),
                        TotalRating = c.Int(nullable: false),
                        RatingCount = c.Int(nullable: false),
                        PageName = c.String(),
                    })
                .PrimaryKey(t => t.VideoId)
                .ForeignKey("dbo.VideoTypes", t => t.VideoTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.VideoTypeId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VideoId = c.Int(nullable: false),
                        PostId = c.Int(),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        Score = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .ForeignKey("dbo.Videos", t => t.VideoId, cascadeDelete: true)
                .Index(t => t.VideoId)
                .Index(t => t.PostId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Language = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Thumbnail = c.Binary(),
                        ThumbnailUrl = c.String(),
                        CategoryId = c.Int(nullable: false),
                        TotalRating = c.Int(nullable: false),
                        RatingCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.VideoTypes",
                c => new
                    {
                        VideoTypeId = c.Int(nullable: false, identity: true),
                        VideoTypeName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VideoTypeId);
            
            CreateTable(
                "dbo.FbVideos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedTime = c.String(),
                        Description = c.String(),
                        EmbedHtml = c.String(),
                        IsAddedToVdoValley = c.Boolean(nullable: false),
                        PageName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.String(),
                        RollNo = c.Int(),
                        Program = c.String(),
                        Subject = c.String(),
                        EmployementDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.PostTags",
                c => new
                    {
                        Post_PostId = c.Int(nullable: false),
                        Tag_TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Post_PostId, t.Tag_TagId })
                .ForeignKey("dbo.Posts", t => t.Post_PostId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .Index(t => t.Post_PostId)
                .Index(t => t.Tag_TagId);
            
            CreateTable(
                "dbo.VideoTag",
                c => new
                    {
                        VideoId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VideoId, t.TagId })
                .ForeignKey("dbo.Videos", t => t.VideoId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.VideoId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Videos", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Videos", "VideoTypeId", "dbo.VideoTypes");
            DropForeignKey("dbo.VideoTag", "TagId", "dbo.Tags");
            DropForeignKey("dbo.VideoTag", "VideoId", "dbo.Videos");
            DropForeignKey("dbo.Ratings", "VideoId", "dbo.Videos");
            DropForeignKey("dbo.PostTags", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.PostTags", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Ratings", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.VideoTag", new[] { "TagId" });
            DropIndex("dbo.VideoTag", new[] { "VideoId" });
            DropIndex("dbo.PostTags", new[] { "Tag_TagId" });
            DropIndex("dbo.PostTags", new[] { "Post_PostId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Ratings", new[] { "ApplicationUserId" });
            DropIndex("dbo.Ratings", new[] { "PostId" });
            DropIndex("dbo.Ratings", new[] { "VideoId" });
            DropIndex("dbo.Videos", new[] { "CategoryId" });
            DropIndex("dbo.Videos", new[] { "VideoTypeId" });
            DropTable("dbo.VideoTag");
            DropTable("dbo.PostTags");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.People");
            DropTable("dbo.FbVideos");
            DropTable("dbo.VideoTypes");
            DropTable("dbo.Tags");
            DropTable("dbo.Posts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Ratings");
            DropTable("dbo.Videos");
            DropTable("dbo.Categories");
        }
    }
}
