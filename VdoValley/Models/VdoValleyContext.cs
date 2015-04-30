using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VdoValley.Models
{
    public class VdoValleyContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public VdoValleyContext() : base("name=VdoValleyContext")
        {
        }

        public System.Data.Entity.DbSet<VdoValley.Models.Video> Videos { get; set; }

        public System.Data.Entity.DbSet<VdoValley.Models.Rating> Ratings { get; set; }
        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            base.OnModelCreating(modelBuilder);

            ///////1-*     Video---Rating
            modelBuilder.Entity<Video>()
                        .HasMany<Rating>(v => v.Ratings)
                        .WithRequired(r => r.Video)
                        .HasForeignKey(r => r.VideoId).WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Video>()
                   .HasMany<Tag>(v => v.Tags)
                   .WithMany(t => t.Videos)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("VideoId");
                       cs.MapRightKey("TagId");
                       cs.ToTable("VideoTag");
                   });
        }

        public System.Data.Entity.DbSet<VdoValley.Models.Tag> Tags { get; set; }
    
    }
}
