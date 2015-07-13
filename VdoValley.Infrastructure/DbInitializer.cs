using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VdoValley.Core.Models;

namespace VdoValley.Infrastructure
{
    public class DbInitializer : DropCreateDatabaseAlways<VdoValleyContext>
    {
        protected override void Seed(VdoValleyContext db)
        {
            db.Videos.Add(new Videoo { Title = "Title 1" });
            db.Videos.Add(new Videoo { Title = "Title 2" });
            db.SaveChanges();
            base.Seed(db);
        }
    }
}
