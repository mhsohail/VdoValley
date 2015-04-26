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
            AutomaticMigrationsEnabled = false;
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
                }

            );
        }
    }
}
