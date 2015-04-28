namespace VdoValley.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VdoValley.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<VdoValley.Models.VdoValleyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VdoValley.Models.VdoValleyContext context)
        {
            if (!(context.ApplicationUsers.Any(u => u.UserName == "fahad.farooq7013@gmail.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "fahad.farooq7013@gmail.com", Email = "fahad.farooq7013@gmail.com", PhoneNumber = "0797697898" };
                userManager.Create(userToInsert, "Fahad@2");
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            VdoValleyContext db = new VdoValleyContext();            
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Videos.AddOrUpdate(
                        new Video
                        {
                            Title = "Best Wedding Dance 2014",
                            Url = "http://dai.ly/x213irw"
                        },

                        new Video
                        {
                            Title = "Saint Mary's Huskies Football",
                            Url = "http://dai.ly/x2o1c02"
                        },

                        new Video
                        {
                            Title = "Paani Wala Dance HD Video Song Kuch Kuch Locha Hai [2015] Sunny Leone",
                            Url = "http://dai.ly/x2m1j8c"
                        },

                        new Video
                        {
                            Title = "Marine's biggest helicopter makes emergency landing in the middle of a beach",
                            Url = "http://dai.ly/x2mw1e9"
                        },

                        new Video
                        {
                            Title = "All about upcoming release 'Pixels'",
                            Url = "http://dai.ly/x2myh6r"
                        },

                        new Video
                        {
                            Title = "Shahid Afridi's exclusive Interview",
                            Url = "http://dai.ly/x2nzrie"
                        },

                        new Video
                        {
                            Title = "Pushing the Limits of BMX w/ Daniel Sandoval",
                            Url = "http://dai.ly/x2nm6pf"
                        },

                        new Video
                        {
                            Title = "Surveillance Video Captures School Bus Crash",
                            Url = "http://dai.ly/x2nx47z"
                        },

                        new Video
                        {
                            Title = "Recycle This Bottled Water Video",
                            Url = "http://dai.ly/x2ny0lg"
                        },

                        new Video
                        {
                            Title = "Food Safety Education - Cook :30",
                            Url = "http://dai.ly/x2nd744"
                        },

                        new Video
                        {
                            Title = "Why Do You Dance? - UNI Dance Marathon 2014",
                            Url = "http://dai.ly/x2msup5"
                        },

                        new Video
                        {
                            Title = "Stage Collapses In The Middle Of Musical",
                            Url = "http://dai.ly/x2ntme0"
                        }
                    );
                    
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }             
        }
    }
}
