using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VdoValley.Core.Models;
using VdoValley.Infrastructure;

namespace VdoValley.Controllers
{
    public class RatingsController : Controller
    {
        private VdoValleyContext db = new VdoValleyContext();

        // GET: Ratings/Create
        public string Createe(Rating rating)
        {
            rating.DateTime = DateTime.Now;
            Dictionary<string, string> response = new Dictionary<string, string>();

            try
            {
                db.Ratings.Add(
                    new Rating
                    {
                        Score = rating.Score,
                        DateTime = rating.DateTime,
                        VideoId = rating.VideoId,
                        ApplicationUserId = rating.ApplicationUserId
                    }
                );
                db.SaveChanges();
                response.Add("success", "1");
                response.Add("message", "Thanks for rating");
            }
            catch(Exception e)
            {
                response.Add("success", "0");
                response.Add("error", e.Message);
            }
            
            return JsonConvert.SerializeObject(response);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
