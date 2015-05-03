using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using VdoValley.Models;
using PagedList.Mvc;
using PagedList;

namespace VdoValley.Controllers
{
    public class HomeController : Controller
    {
        VdoValleyContext db = new VdoValleyContext();
        public ActionResult Index(int? page)
        {
            var store = new UserStore<ApplicationUser>(db);
            var userM = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userM.FindByNameAsync("fahad.farooq7013@gmail.com").Result;
            List<Video> dbVideos = db.Videos.ToList();
            
            Video v5 = new Video
            {
                Title = "Shahid Afridi Telling Funny Story Of How He Got Married - Watch Video",
                Description = "Description 5",
                Url = "http://www.dailymotion.com/video/x2nxjqb_shahid-afridi-telling-funny-story-of-how-he-got-married-watch-video_news"
            };

            return View(db.Videos.ToList().ToPagedList(page ?? 1, 12));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string q)
        {
            return View(db.Videos.Where(v => v.Title.Contains(q)).ToList());
        }

    }
}