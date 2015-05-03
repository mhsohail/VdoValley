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
                            CategoryId = cat.CategoryId,
                            Featured = true
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
                        },
                        
                        new Video
                        {
                            Title = "Amazing trick with Eggs for Home use",
                            Url = "http://dai.ly/x2on26a",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Can You Walk on Water (Non-Newtonian Fluid Pool)",
                            Url = "http://dai.ly/x2on01r",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Outside Television 2015: What's Next",
                            Url = "http://dai.ly/x2oldaf",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Menswear dog styles four pooches",
                            Url = "http://dai.ly/x2omivj",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Epic Domino Etch A Sketch Will Make You Say 'Wow!'",
                            Url = "http://dai.ly/x2opv71",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Sunset skater (private)",
                            Url = "http://dai.ly/x2opv71",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Nepal Earthquack Real LIVE footage",
                            Url = "http://dai.ly/x2o4cc7",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Karan Johan's hilarious joke on Anuragh Kashyap",
                            Url = "http://dai.ly/x2okcl6",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "There's Something Oddly Satisfying About Watching 10,000 Marbles Roll",
                            Url = "http://dai.ly/x2oqwej",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Flowers Burst Into Full Bloom in Beautiful Time-Lapse Video",
                            Url = "http://dai.ly/x2opusq",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "How to Get a Baby to Fall Asleep in Less Than One Minute",
                            Url = "http://dai.ly/x2oput0",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Cliff Diving in the Seaside Town Of Cartagena, Colombia",
                            Url = "http://dai.ly/x2om451",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Red Bull Hart Lines 2015",
                            Url = "http://dai.ly/x2o79ns",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Just Cause 3 | Gameplay Reveal Trailer (Xbox One)",
                            Url = "http://dai.ly/x2oa6c0",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "A night in the streets with Baltimore protesters",
                            Url = "http://dai.ly/x2o9w6j",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "nepal earthquake still photos",
                            Url = "http://dai.ly/x2nxoih",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Footage Of Everest Avalanche Caused By Aftershock From Nepal Earthquake",
                            Url = "http://dai.ly/x2o4l7d",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "Breaking_ 7.5 magnitude earthquake hits Nepal",
                            Url = "http://dai.ly/x2nxklp",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "This video of Nepal Earthquake Tells the whole store",
                            Url = "http://dai.ly/x2nym17",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "LG G4 Wonderful Video HD",
                            Url = "http://dai.ly/x2oe21d",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "WOW: First Ever Triple Backflip on a Motorcycle",
                            Url = "http://dai.ly/x2oiahw",
                            DateTime = DateTime.Now,
                            CategoryId = cat.CategoryId
                        },

                        new Video
                        {
                            Title = "This Cigarette Lighter is Magic!",
                            Url = "http://dai.ly/x2lncey",
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
