using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VdoValley.Models;

namespace VdoValley.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Video = getDailyMotionThumb("x2nzrie", DailymotionThumbnailSize.thumbnail_large_url.ToString());
            return View();
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

        [NonAction]
        public string getDailyMotionThumb(string video_code, string thumbnail_size)
        {
            string json = string.Empty;
            using (var client = new WebClient())
            {
                json = client.DownloadString("https://api.dailymotion.com/video/" + video_code + "?fields=" + thumbnail_size);
            }

            string obj;
            obj = JsonConvert.DeserializeObject(json).ToString();
            
            return json;
        }
    }
}