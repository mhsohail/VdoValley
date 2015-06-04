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
using VdoValley.ViewModels;
using System.Threading.Tasks;
using Microsoft.Web.Administration;
using VdoValley.Attributes;

namespace VdoValley.Controllers
{
    public class HomeController : Controller
    {
        VdoValleyContext db = new VdoValleyContext();
        public ActionResult Index(int? page)
        {
            HomeViewModel hvm = new HomeViewModel();
            hvm.AllVideos = db.Videos.ToList();

            IPagedList<Video> videos = null;
            try
            {
                videos = db.Videos.ToList().ToPagedList(page ?? 1, 24);
            }
            catch(Exception e)
            {
                
            }

            return View(videos);
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

        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult FacebookImport()
        {
            return View();
        }

        [AjaxRequestOnly]
        public string ImportFacebookVideo([Bind(Include = "EmbedId,Title,Description,EmbedCode,PageName,VideoTypeId")] Video video)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            
            try
            {
                var ii = (int)VideoTypeEnum.DAILYMOTION;
                var jj = VideoTypeEnum.DAILYMOTION.ToString();
                if (int.Parse("12") == video.VideoTypeId)
                { }
                
                video.EmbedCode = WebUtility.HtmlDecode(video.EmbedCode);
                video.DateTime = DateTime.Now;
                video.Featured = false;
                video.CategoryId = 1;
                if (video.Title == null)
                {
                    video.Title = video.Description;
                }

                var videoInDb = db.Videos.FirstOrDefault(v => v.EmbedId.Equals(video.EmbedId));
                if (videoInDb != null)
                {
                    response["success"] = true;
                    response["message"] = "Video already added to VdoValley.";
                    return JsonConvert.SerializeObject(response);
                }
                db.Videos.Add(video);
                db.SaveChanges();
                
                response["success"] = true;
                response["message"] = "Video saved.";
                return JsonConvert.SerializeObject(response);
            }
            catch(Exception exc)
            {
                response["success"] = false;
                response["message"] = "Video saving failed.";
                return JsonConvert.SerializeObject(response);
            }
        }

        public ActionResult GetFacebookPageVideos(string EmbedId)
        {
            string json = string.Empty;
            List<FbVideo> fbVideos = new List<FbVideo>();

            try
            {
                using (var client = new WebClient())
                {
                    json = client.DownloadString("https://graph.facebook.com/" + EmbedId + "/videos");
                }

                var videosData = new
                {
                    data = new[]
                    {
                        new
                        {
                            id = string.Empty,
                            created_time = string.Empty,
                            description = string.Empty,
                            embed_html = string.Empty,
                            from = new {
                                category = string.Empty, 
                                name = string.Empty, 
                                id = string.Empty
                            }
                        }
                    }
                };

                var videos = JsonConvert.DeserializeAnonymousType(json, videosData);

                for (int i = 0; i < videos.data.Length; i++)
                {
                    FbVideo fbVideo = new FbVideo();
                    fbVideo.Id = videos.data[i].id;
                    fbVideo.CreatedTime = videos.data[i].created_time;
                    if (videos.data[i].description != null)
                    {
                        fbVideo.Description = videos.data[i].description.PadRight(100).Substring(0, 100) + ((videos.data[i].description.Length > 100) ? "..." : "");
                    }
                    fbVideo.EmbedHtml = WebUtility.HtmlEncode(videos.data[i].embed_html);
                    fbVideo.PageName = videos.data[i].from.name;

                    var vdo = db.Videos.FirstOrDefault(v => v.EmbedId == fbVideo.Id);
                    if (vdo == null)
                    {
                        fbVideo.IsAddedToVdoValley = false;
                    }
                    else
                    {
                        fbVideo.IsAddedToVdoValley = true;
                    }

                    fbVideos.Add(fbVideo);
                }

                return PartialView("_GetFacebookPageVideos", fbVideos);
            }
            catch (Exception exc)
            {
                return PartialView("_GetFacebookPageVideos", fbVideos);
            }
        }

        public ActionResult ImportDailymotionVideos()
        {
            var VideoTypeId = db.VideoTypes.FirstOrDefault(vt => vt.VideoTypeName == VideoTypeEnum.DAILYMOTION).VideoTypeId;
            ViewBag.VideoTypeId = VideoTypeId;
            return View();
        }
    }
}