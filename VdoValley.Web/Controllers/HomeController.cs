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
using VdoValley.Web.ViewModels;
using System.Threading.Tasks;
using VdoValley.Attributes;
using AuthorizeNet;
using VdoValley.Infrastructure;
using VdoValley.Web.Controllers;
using VdoValley.Web.ViewModels;
using System.IO;
using VdoValley.Infrastructure.Repositories;
using VdoValley.Web.Helpers;

namespace VdoValley.Controllers
{
    public class HomeController : Controller
    {
        VdoValleyContext db = new VdoValleyContext();
        VideosRepository VideosRepo = new VideosRepository();
        CategoryRepository CategoryRepo = new CategoryRepository();

        public ActionResult Index(int? page)
        {
            HomeViewModel hvm = new HomeViewModel();
            IPagedList<Video> videos = null;
            try
            {
                //videos = db.Videos.ToList().ToPagedList(page ?? 1, 24);
                
                hvm.AllVideos = VideosRepo.GetVideos();
                hvm.FashionVideos = CategoryRepo.GetCategories().FirstOrDefault(c => c.CategoryId == 15).Videos.OrderByDescending(v => v.DateTime).Take(8);
                hvm.FunnyVideos = CategoryRepo.GetCategories().FirstOrDefault(c => c.CategoryId == 12).Videos.OrderByDescending(v => v.DateTime).Take(8);
                hvm.PoliticsVideos = CategoryRepo.GetCategories().FirstOrDefault(c => c.CategoryId == 13).Videos.OrderByDescending(v => v.DateTime).Take(8);
                hvm.SportsVideos = CategoryRepo.GetCategories().FirstOrDefault(c => c.CategoryId == 14).Videos.OrderByDescending(v => v.DateTime).Take(8);
                hvm.TalkShowVideos = CategoryRepo.GetCategories().FirstOrDefault(c => c.CategoryId == 16).Videos.OrderByDescending(v => v.DateTime).Take(8);
                hvm.NewsVideos = CategoryRepo.GetCategories().FirstOrDefault(c => c.CategoryId == 17).Videos.OrderByDescending(v => v.DateTime).Take(8);
                hvm.LatestVideos = VideosRepo.GetLatestVideos(0, 10);
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

        [HttpPost]
        [AjaxRequestOnly]
        public void ImportZemTvVideos(int page)
        {
            MatchCollection aMatchCollection = null;
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                string htmlCode = client.DownloadString("http://www.zemtv.com/category/user-submitted/page/" + page + "/");
                string Pattern = "(?<=<div class=\"thumbnail\">[\\r\\n\\s]*<a href=\")http://www.zemtv.com/[0-9]{4}/[0-9]{2}/[0-9]{2}/[a-zA-Z_0-9-]+";
                Regex aRegex = new Regex(Pattern, RegexOptions.IgnoreCase);
                aMatchCollection = aRegex.Matches(htmlCode);
                foreach (Match aMatch in aMatchCollection.Cast<Match>().Reverse())
                {
                    using (WebClient client2 = new WebClient()) // WebClient class inherits IDisposable
                    {
                        htmlCode = client.DownloadString(aMatch.Value);
                        Pattern = "<iframe.+src=\"http://www.dailymotion.com/embed/video/(\\w+)?.+\"></iframe>";
                        aRegex = new Regex(Pattern, RegexOptions.IgnoreCase);
                        Match aMatch2 = aRegex.Match(htmlCode);
                        if (aMatch2.Success)
                        {
                            var VideoCode = aMatch2.Groups[1].Value;
                            if (VideoHelper.VideoExists(VideoCode)) continue;
                            //"https://api.dailymotion.com/video/" + videoCode + "?fields=id,title,description"
                            string serviceUrl = string.Format("https://api.dailymotion.com/video/" + VideoCode + "?fields=id,title,description");
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
                            request.Method = "GET";
                            request.Accept = "application/json; charset=UTF-8";

                            try
                            {
                                var httpResponse = (HttpWebResponse)request.GetResponse();
                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                {
                                    var responstText = streamReader.ReadToEnd();
                                    //Receipt = serializer.Deserialize<Receipt>(responstText);
                                    var DMVideo = new
                                    {
                                        Id = string.Empty,
                                        Title = string.Empty,
                                        Description = string.Empty
                                    };

                                    DMVideo = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(responstText, DMVideo);
                                    var SuggestedTags = TagRepository.GetSuggestedTagsForTitle(DMVideo.Title, string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")));
                                    VdoValley.Web.ViewModels.VideoViewModel vvm = new VdoValley.Web.ViewModels.VideoViewModel();

                                    var SuggestedTagsStr = string.Empty;
                                    if (SuggestedTags.Count > 0) SuggestedTagsStr = SuggestedTags[0].Name;
                                    foreach (var SuggestedTag in SuggestedTags.Skip(1))
                                    {
                                        SuggestedTagsStr += "," + SuggestedTag.Name;
                                    }

                                    vvm.Tags = SuggestedTagsStr;
                                    vvm.Title = DMVideo.Title;
                                    vvm.EmbedId = DMVideo.Id;
                                    vvm.Description = DMVideo.Description;
                                    vvm.VideoTypeId = 1;
                                    vvm.Url = "http://dai.ly/" + DMVideo.Id;
                                    vvm.SelectedCategory = 1;
                                    vvm.Featured = false;
                                    vvm.IsJsonRequest = true;
                                    vvm.IsAutoImported = true;

                                    var vvv = new VideosController().Create(vvm);
                                    int a = 9;
                                }
                            }
                            catch(WebException WebExc) { }
                            catch (Exception Exc) { }
                        }
                    }
                }
            }
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
        public string ImportFacebookVideo([Bind(Include = "EmbedId,Title,Description,EmbedCode,PageName,VideoTypeId,SelectedCategory,Tags,IsJsonRequest")] VideoViewModel vvm)
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
        
    }
}