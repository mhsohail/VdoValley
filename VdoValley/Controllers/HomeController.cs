using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using VdoValley.Models;

namespace VdoValley.Controllers
{
    public class HomeController : Controller
    {
        VdoValleyContext db = new VdoValleyContext();
        public ActionResult Index()
        {
            List<Video> dbVideos = db.Videos.ToList();
            
            Video v1 = new Video
            {
                Title = "Zenit St Petersburg vs Sevilla - Highlights - UEFA Europa League - PES 15 - Round of April 23",
                Description = "Description 1",
                Url = "http://www.dailymotion.com/video/x2nq7ae_zenit-st-petersburg-vs-sevilla-highlights-uefa-europa-league-pes-15-round-of-april-23_videogames"
            };
        
            Video v2 = new Video
            {
                Title = "Burning Rubber in the Streets of Vienna - Red Bull Show Run 2015",
                Description = "Description 2",
                Url = "http://www.dailymotion.com/video/x2nv88j_burning-rubber-in-the-streets-of-vienna-red-bull-show-run-2015_auto"
            };

            Video v3 = new Video
            {
                Title = "This Video Clearly Shows What Kind of Relations Model Ayyan Ali Had with Asif Zardari",
                Description = "Description 3",
                Url = "http://www.dailymotion.com/video/x2o0vel_this-video-clearly-shows-what-kind-of-relations-model-ayyan-ali-had-with-asif-zardari_news#from=embediframe"
            };
            
            Video v4 = new Video
            {
                Title = "Fox News Making Fun Of Unsuccessful Indian Missile Test",
                Description = "Description 4",
                Url = "http://www.dailymotion.com/video/x2nux9t_fox-news-making-fun-of-unsuccessful-indian-missile-test_news"
            };

            Video v5 = new Video
            {
                Title = "Shahid Afridi Telling Funny Story Of How He Got Married - Watch Video",
                Description = "Description 5",
                Url = "http://www.dailymotion.com/video/x2nxjqb_shahid-afridi-telling-funny-story-of-how-he-got-married-watch-video_news"
            };
            
            v1.thumbnail_large_url = getDailyMotionThumb("x2nq7ae", DailymotionThumbnailSize.thumbnail_large_url);
            v2.thumbnail_large_url = getDailyMotionThumb("x2nv88j", DailymotionThumbnailSize.thumbnail_large_url);
            v3.thumbnail_large_url = getDailyMotionThumb("x2o0vel", DailymotionThumbnailSize.thumbnail_large_url);
            v4.thumbnail_large_url = getDailyMotionThumb("x2nux9t", DailymotionThumbnailSize.thumbnail_large_url);
            v5.thumbnail_large_url = getDailyMotionThumb("x2nxjqb", DailymotionThumbnailSize.thumbnail_large_url);

            List<Video> videos = new List<Video>()
            {
                v1,v2,v3,v4,v5
            };

            foreach(Video video in dbVideos)
            {
                video.thumbnail_large_url = getDailyMotionThumb(getDailyMotionViewCode(video.Url), DailymotionThumbnailSize.thumbnail_large_url);
            }
            
            ViewData["videos"] = dbVideos;
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
        public string getDailyMotionThumb(string video_code, DailymotionThumbnailSize thumbnail_size)
        {
            string size = thumbnail_size.ToString();
            string json = string.Empty;
            using (var client = new WebClient())
            {
                json = client.DownloadString("https://api.dailymotion.com/video/" + video_code + "?fields=" + size);
            }

            Video vdo = JsonConvert.DeserializeObject<Video>(json);
            return vdo.thumbnail_large_url;
        }

        public string getDailyMotionViewCode(string short_url)
        {
            string code = string.Empty;
            string pattern = @"(http://dai.ly/)(\S*)";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(short_url);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    code = match.Value;
                }
            }

            return matches[0].Groups[2].Value;
        }
    }
}