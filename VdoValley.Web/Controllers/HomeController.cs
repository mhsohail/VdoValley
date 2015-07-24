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
using VdoValley.Core.Models;
using PagedList.Mvc;
using PagedList;
using VdoValley.ViewModels;
using System.Threading.Tasks;
using VdoValley.Attributes;
using AuthorizeNet;
using VdoValley.Infrastructure;
using VdoValley.Web.Controllers;
using VdoValley.Web.ViewModels;

namespace VdoValley.Controllers
{
    public class HomeController : Controller
    {
        VdoValleyContext db = new VdoValleyContext();
        VideoRepository VideoRepo = new VideoRepository();

        public ActionResult Index(int? page)
        {
            HomeViewModel hvm = new HomeViewModel();
            IPagedList<Video> videos = null;
            try
            {
                //videos = db.Videos.ToList().ToPagedList(page ?? 1, 24);
                
                hvm.AllVideos = VideoRepo.GetVideos();
                hvm.FashionVideos = db.Categories.FirstOrDefault(c => c.CategoryId == 15).Videos.OrderByDescending(v => v.DateTime).Take(8);
                hvm.FunnyVideos = db.Categories.FirstOrDefault(c => c.CategoryId == 12).Videos.OrderByDescending(v => v.DateTime).Take(8);
                hvm.PoliticsVideos = db.Categories.FirstOrDefault(c => c.CategoryId == 13).Videos.OrderByDescending(v => v.DateTime).Take(8);
                hvm.SportsVideos = db.Categories.FirstOrDefault(c => c.CategoryId == 14).Videos.OrderByDescending(v => v.DateTime).Take(8);
                hvm.TalkShowVideos = db.Categories.FirstOrDefault(c => c.CategoryId == 16).Videos.OrderByDescending(v => v.DateTime).Take(8);
                hvm.NewsVideos = db.Categories.FirstOrDefault(c => c.CategoryId == 17).Videos.OrderByDescending(v => v.DateTime).Take(8);
            }
            catch(Exception e)
            {
                
            }

            return View(hvm);
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
        public string ImportFacebookVideo([Bind(Include = "EmbedId,Title,Description,EmbedCode,PageName,VideoTypeeId,SelectedCategory,Tags,IsJsonRequest")] VideoViewModel vvm)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            var vvv = new VideosController().Create(vvm);

            try
            {
                //video.EmbedCode = WebUtility.HtmlDecode(video.EmbedCode);
                //if (video.VideoTypeId == 1) // id dailymotion video
                //{
                //    video.Url = "http://dai.ly/" + video.EmbedId;
                //    video.EmbedCode = string.Empty;
                //}
                //
                //if (video.VideoTypeId != 1) // id fb video
                //{
                //    video.CategoryId = 1;
                //}
                //
                //video.DateTime = DateTime.Now;
                //video.Featured = false;
                //
                //if (video.Title == null)
                //{
                //    video.Title = video.Description;
                //}
                //
                //var videoInDb = db.Videos.FirstOrDefault(v => v.EmbedId.Equals(video.EmbedId));
                //if (videoInDb != null)
                //{
                //    response["success"] = true;
                //    response["message"] = "Video already added to VdoValley.";
                //    return JsonConvert.SerializeObject(response);
                //}
                //db.Videos.Add(video);
                //db.SaveChanges();
                //
                //response["success"] = true;
                //response["message"] = "Video saved.";
                return JsonConvert.SerializeObject(vvv);
            }
            catch(Exception exc)
            {
                //response["success"] = false;
                //response["message"] = "Video saving failed.";
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
            var VideoTypeId = 0;
            try
            {
                VideoTypeId = db.VideoTypes.FirstOrDefault(vt => vt.VideoTypeName == VideoTypeEnum.DAILYMOTION).VideoTypeId;
                ViewBag.VideoTypeId = VideoTypeId;
            }
            catch(Exception exc)
            {
            
            }
            
            return View();
        }

        public ActionResult DPM()
        {
            String ApiLogin = "2Vnc7H75By";
            String TxnKey = "9KnE8M58n694B2qR";

            String DPMFormOpen = DPMFormGenerator.OpenForm(ApiLogin, TxnKey, 10.25M, "http://vdovalley.com/Home/DPMResponse", true);
            String DPMFormEnd = DPMFormGenerator.EndForm();
            ViewBag.DPMFormOpen = DPMFormOpen;
            ViewBag.DPMFormEnd = DPMFormEnd;

            return View();
        }

        public ActionResult DPMReceipt()
        {
            return Content("<html><h1>Thank You!</h1></html>");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DPMResponse(FormCollection post)
        {
            var response = new SIMResponse(post);
            var hash = "202cb962ac59075b964b07152d234b70";

            // First order of business - validate that it was Authorize.Net 
            // that posted this using the MD5 hash that was passed back to us
            var isValid = response.Validate(hash, "2Vnc7H75By");

            // If it's not valid - just send them to the home page. 
            if (!isValid)
                return Redirect("/");
            // The URL to redirect to MUST be absolute
            //var returnUrl = "http://vdovalley.com/Home/DPMReceipt?m=" + response.Message;
            var returnUrl = "http://vdovalley.com/Home/DPMReceipt";

            return Content(string.Format("<html><head><script type='text/javascript' charset='utf-8'>window.location='{0}';</script><noscript><meta http-equiv='refresh' content='1;url={0}'></noscript></head><body></body></html>", returnUrl));
        }

    }
}