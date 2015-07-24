using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VdoValley.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace VdoValley.Infrastructure
{
    public class VdoValleyContext : IdentityDbContext<ApplicationUser>
    {
        public VdoValleyContext()
            : base("VdoValleyContext")
        {
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<IdentityUserLogin> UserLogins { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            ///////1-*     Video---Rating
            modelBuilder.Entity<Video>()
                        .HasMany<Rating>(v => v.Ratings)
                        .WithRequired(r => r.Video)
                        .HasForeignKey(r => r.VideoId).WillCascadeOnDelete(true);

            ///////1-*     Category---Video
            modelBuilder.Entity<Category>()
                        .HasMany<Video>(c => c.Videos)
                        .WithRequired(v => v.Category)
                        .HasForeignKey(v => v.CategoryId).WillCascadeOnDelete(true);

            ///////*-*     Video---Tags
            modelBuilder.Entity<Video>()
                   .HasMany<Tag>(v => v.Tags)
                   .WithMany(t => t.Videos)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("VideoId");
                       cs.MapRightKey("TagId");
                       cs.ToTable("VideoTag");
                   });

            ///////*-*     Post---Tags
            modelBuilder.Entity<Post>()
                   .HasMany<Tag>(p => p.Tags)
                   .WithMany(t => t.Posts)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("Post_PostId");
                       cs.MapRightKey("Tag_TagId");
                       cs.ToTable("PostTags");
                   });

            /////1-*     User---Rating
            modelBuilder.Entity<ApplicationUser>()
                        .HasMany<Rating>(au => au.Ratings)
                        .WithRequired(r => r.ApplicationUser)
                        .HasForeignKey(r => r.ApplicationUserId).WillCascadeOnDelete(true);

            ///////1-*     VideoType---Videos
            modelBuilder.Entity<VideoType>()
                        .HasMany<Video>(vt => vt.Videos)
                        .WithOptional(v => v.VideoType)
                        .HasForeignKey(v => v.VideoTypeId).WillCascadeOnDelete(true);
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<VideoType> VideoTypes { get; set; }
        public DbSet<FbVideo> FbVideos { get; set; }
        public DbSet<Post> Posts { get; set; }   
    }
}
