namespace VdoValley.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VdoValley.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<VdoValley.Models.VdoValleyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(VdoValley.Models.VdoValleyContext context)
        {
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
                    Category cat = new Category();
                    cat.Name = "General Category";
                    db.Categories.AddOrUpdate(cat);
                    db.SaveChanges();

                    db.Videos.AddOrUpdate(
                        new Video
                        {
                            Title = "Best Wedding Dance 2014",
                            Url = "http://dai.ly/x213irw",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Saint Mary's Huskies Football",
                            Url = "http://dai.ly/x2o1c02",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Paani Wala Dance HD Video Song Kuch Kuch Locha Hai [2015] Sunny Leone",
                            Url = "http://dai.ly/x2m1j8c",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Marine's biggest helicopter makes emergency landing in the middle of a beach",
                            Url = "http://dai.ly/x2mw1e9",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "All about upcoming release 'Pixels'",
                            Url = "http://dai.ly/x2myh6r",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Shahid Afridi's exclusive Interview",
                            Url = "http://dai.ly/x2nzrie",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Pushing the Limits of BMX w/ Daniel Sandoval",
                            Url = "http://dai.ly/x2nm6pf",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Surveillance Video Captures School Bus Crash",
                            Url = "http://dai.ly/x2nx47z",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Recycle This Bottled Water Video",
                            Url = "http://dai.ly/x2ny0lg",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Food Safety Education - Cook :30",
                            Url = "http://dai.ly/x2nd744",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Why Do You Dance? - UNI Dance Marathon 2014",
                            Url = "http://dai.ly/x2msup5",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Stage Collapses In The Middle Of Musical",
                            Url = "http://dai.ly/x2ntme0",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
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
