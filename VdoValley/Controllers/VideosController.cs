using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VdoValley.Models;
using Microsoft.AspNet.Identity;
using VdoValley.ViewModels;
using VdoValley.Attributes;
using Microsoft.AspNet.Identity.EntityFramework;
using VdoValley.Helpers;
using Newtonsoft.Json;
using VdoValley.Infrastructure;

namespace VdoValley.Controllers
{
    public class VideosController : Controller
    {
        VideoRepository repo;
        private VdoValley.Models.VdoValleyContext db = new VdoValley.Models.VdoValleyContext();

        public VideosController()
        {
            repo = new VideoRepository();
        }

        // GET: Videos
        public ActionResult Index()
        {
            return View(db.Videos.ToList());
        }

        // GET: Videos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }

            //HttpContext.Current.User.Identity.GetUserId();
            ViewBag.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (video.Url == null)
            {
                if (video.ThumbnailURL == null)
                {
                    ViewBag.OgThumbnail = VdoValley.Helpers.VideoHelper.getFacebookVideoThumbnail(video, VdoValley.Helpers.VideoHelper.getFacebookVideoCode(video, video.EmbedCode), VdoValley.Models.FacebookThumbnailSize.MEDIUM).Replace("amp;", string.Empty);
                    //ViewBag.OgThumbnail = video.getFacebookVideoThumbnail(video.getFacebookVideoCode(video.EmbedCode), VdoValley.Models.FacebookThumbnailSize.MEDIUM).Replace("amp;", string.Empty);
                }
                else
                {
                    ViewBag.OgThumbnail = video.ThumbnailURL;
                }
            }
            else
            {
                if (video.ThumbnailURL == null)
                {
                    ViewBag.OgThumbnail = VideoHelper.getDailyMotionThumb(video, VideoHelper.getDailyMotionVideoCode(video, video.Url), VdoValley.Models.DailymotionThumbnailSize.thumbnail_large_url);
                    //ViewBag.OgThumbnail = video.getDailyMotionThumb(video.getDailyMotionVideoCode(video.Url), VdoValley.Models.DailymotionThumbnailSize.thumbnail_large_url);
                }
                else
                {
                    ViewBag.OgThumbnail = video.ThumbnailURL;
                }
            }

            List<Rating> ratings = db.Ratings.Where(r => r.VideoId == video.VideoId).ToList();
            int TotalRating = 0;
            foreach (Rating rating in ratings)
            {
                TotalRating += rating.Score;
            }
            
            double AvgRating = TotalRating / (double)ratings.Count;
            AvgRating = Math.Round(AvgRating, 2);
            if (double.IsNaN(AvgRating)) { AvgRating = 0.0; }

            // get logged in user and its score for this video
            string LoggedInUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            int CurrentUsersScore = 0;
            VdoValley.Models.Rating VideoScorer = db.Ratings.FirstOrDefault(r => r.ApplicationUserId.Equals(LoggedInUserId) && r.VideoId.Equals(video.VideoId));
            if (VideoScorer != null)
            {
                CurrentUsersScore = VideoScorer.Score;
            }
            
            VideoDetailsViewModel vdvm = new VideoDetailsViewModel();
            vdvm.Video = video;
            vdvm.AverageRating = AvgRating;
            vdvm.CurrentUsersScore = CurrentUsersScore;
            vdvm.VideoScorer = VideoScorer;

            var relatedVideos = new List<Video>();
            var tags = video.Tags;
            foreach (var tag in tags)
            {
                // get videos for curren tag
                foreach (var vdo in tag.Videos)
                {
                    if(vdo.VideoId != video.VideoId && !relatedVideos.Contains(vdo))
                    {
                        relatedVideos.Add(vdo);
                    }
                    if (relatedVideos.Count == 8) break;
                }
                if (relatedVideos.Count == 8) break;
            }
            vdvm.RelatedVideos = relatedVideos;

            return View(vdvm);
        }

        // GET: Videos/Create
        public ActionResult Create()
        {
            TempData["Categories"] = db.Categories.ToList();
            TempData["VideoTypes"] = db.VideoTypes.ToList();
            VideoViewModel vvm = new VideoViewModel();
            return View(vvm);
        }

        // POST: Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Url,EmbedCode,EmbedId,Description,Tags,SelectedCategory,VideoTypeId,PageName,Featured,ThumbnailURL,IsJsonRequest")] VideoViewModel vvm)
        {
            if (ModelState.IsValid)
            {
                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        vvm.DateTime = DateTime.Now;
                        vvm.Tags = (vvm.Tags == null) ? vvm.Tags : Uri.UnescapeDataString(vvm.Tags);
                        var video = ViewModelHelpers.VMHelper.ToDomainVideoModel(vvm);
                        video.EmbedCode = WebUtility.HtmlDecode(video.EmbedCode);

                        if (video.VideoTypeId == 1) // if dailymotion video
                        {
                            if (video.Url == null && video.EmbedId != null)
                            {
                                video.Url = "http://dai.ly/" + video.EmbedId;
                            }

                            if (video.EmbedId == null && video.Url != null)
                            {
                                video.EmbedId = VideoHelper.getDailyMotionVideoCode(video, video.Url);
                            }
                            
                            video.EmbedCode = null;
                        }
                        
                        if (video.VideoTypeId != 1) // if fb video
                        {
                            if (video.CategoryId == null)
                            {
                                video.CategoryId = 1;
                            }
                        }

                        if (video.Title == null)
                        {
                            video.Title = video.Description;
                        }

                        if (VideoHelper.VideoExists(video))
                        {
                            // if this video already exists in database
                            /*
                            response["success"] = true;
                            response["message"] = "Video already added to VdoValley.";
                            return JsonConvert.SerializeObject(response);
                            */
                        }
                        else
                        {
                            if (video.Featured == true)
                            {
                                var FeaturedVideos = db.Videos.Where(v => v.Featured == true);
                                foreach (var fv in FeaturedVideos)
                                {
                                    if (fv != video) // if the featured video is not the current video being posted
                                    {
                                        fv.Featured = false;
                                    }
                                }
                            }

                            List<string> tags = new List<string>();
                            if (vvm.Tags != null)
                            {
                                tags = vvm.Tags.Split(',').ToList();
                            }

                            List<Tag> tagsToAdd = new List<Tag>();
                            foreach (var tag in tags)
                            {
                                Tag newTag = new Tag();
                                newTag.Name = tag.Trim().ToLower();
                                var tagInDb = db.Tags.FirstOrDefault(t => t.Name.Equals(tag));
                                if (tagInDb == null)
                                {
                                    tagsToAdd.Add(newTag);
                                }
                                else
                                {
                                    tagsToAdd.Add(tagInDb);
                                }
                            }
							
                            db.Videos.Add(video);
                            db.SaveChanges();
                            video.Tags = new List<Tag>();
                            foreach (var tag in tagsToAdd)
                            {
                                if (tag.TagId.Equals(0))
                                {
                                    db.Tags.Add(tag);
                                    db.SaveChanges();
                                }

                                db.Tags.Attach(tag);
                                video.Tags.Add(tag);
                                db.SaveChanges();
                            }

                            tran.Commit();
                            if (vvm.IsJsonRequest)
                            {
                                Dictionary<string, object> VideoDTO = new Dictionary<string, object>();
                                VideoDTO.Add("Id", video.VideoId);
                                VideoDTO.Add("Title", video.Title);
                                VideoDTO.Add("Description", video.Description);

                                if (video.VideoTypeId == 1)
                                {
                                    if (video.EmbedId != null && video.ThumbnailURL == null)
                                    {
                                        VideoDTO.Add("ThumbnailURL", VideoHelper.getDailyMotionThumb(video, video.EmbedId, DailymotionThumbnailSize.thumbnail_large_url));
                                    }
                                    else
                                    {
                                        VideoDTO.Add("ThumbnailURL", video.ThumbnailURL);
                                    }
                                }

                                return RedirectToAction(JsonConvert.SerializeObject(VideoDTO));
                            }
                            else
                            {
                                return RedirectToAction("Details/" + video.VideoId);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.ToString());
                        tran.Rollback();
                    }
                }

                /*
                    Value cannot be null.
                    Parameter name: items
                    Description: An unhandled exception occurred during the execution of the current web request. Please review the stack trace for more information about the error and where it originated in the code. 
                */
                // this line is to avoid above error
                // if the form is not valid or some other error occurs, the form is displayed again,
                // and the TempData should be available again to populate the dropdown list,
                // that's why, this line is required.
                TempData["Categories"] = db.Categories.ToList();
                
                return View(vvm);
            }

            return View(vvm);
        }

        // GET: Videos/Edit/5
        [Authorize(Roles="Administrator")]
        public ActionResult Edit(int? id)
        {
            //return View();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }

            VideoViewModel vvm = new VideoViewModel();
            vvm.VideoId = video.VideoId;
            vvm.Url = video.Url;
            vvm.EmbedCode = video.EmbedCode;
            vvm.Title = video.Title;
            vvm.SelectedCategory = video.CategoryId;
            vvm.Description = video.Description;
            vvm.DateTime = video.DateTime;
            vvm.Featured = video.Featured;
            vvm.ThumbnailURL = video.ThumbnailURL;
            vvm.VideoTypeId = int.Parse(video.VideoTypeId.ToString());
            
            TempData["Categories"] = db.Categories.ToList();
            TempData["VideoTypes"] = db.VideoTypes.ToList();

            return View(vvm);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VideoId,Title,Url,EmbedCode,EmbedId,Description,Tags,SelectedCategory,VideoTypeId,PageName,Featured,ThumbnailURL,IsJsonRequest")] VideoViewModel vvm)
        {
            //return View();
            Video video = null;
            if (ModelState.IsValid)
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // get the domain model from VM
                        video = ViewModelHelpers.VMHelper.ToDomainVideoModel(vvm);

                        if (video.VideoTypeId == 1) // if dailymotion video
                        {
                            if (video.Url == null && video.EmbedId != null)
                            {
                                video.Url = "http://dai.ly/" + video.EmbedId;
                            }

                            if (video.EmbedId == null && video.Url != null)
                            {
                                video.EmbedId = VideoHelper.getDailyMotionVideoCode(video, video.Url);
                            }

                            video.EmbedCode = null;
                        }

                        if (video.VideoTypeId != 1) // id fb video
                        {
                            if (video.CategoryId == null)
                            {
                                video.CategoryId = 1;
                            }
                        }

                        if (video.Title == null)
                        {
                            video.Title = video.Description;
                        }

                        if (video.Featured == true)
                        {
                            var FeaturedVideos = db.Videos.Where(v => v.Featured == true);
                            foreach(var fv in FeaturedVideos)
                            {
                                if (fv != video) // if the featured video is not the current video being posted
                                {
                                    fv.Featured = false;
                                }
                            }
                        }

                        // get tags
                        vvm.Tags = (vvm.Tags == null) ? vvm.Tags : Uri.UnescapeDataString(vvm.Tags);
                        List<string> updatedTags = new List<string>();
                        if (vvm.Tags != null)
                        {
                            updatedTags = vvm.Tags.Split(',').ToList();
                        }

                        // load current video from database including tags
                        //db.Configuration.ProxyCreationEnabled = false;
                        var vdo = db.Videos.Include(v => v.Tags).FirstOrDefault(v => v.VideoId.Equals(video.VideoId));
                        vdo.Title = video.Title;
                        vdo.Url = video.Url;
                        vdo.EmbedCode = video.EmbedCode;
                        vdo.EmbedId= video.EmbedId;
                        vdo.Description = video.Description;
                        vdo.VideoTypeId = video.VideoTypeId;
                        vdo.CategoryId = video.CategoryId;
                        vdo.Featured = video.Featured;
                        vdo.ThumbnailURL = video.ThumbnailURL;
                        vdo.PageName = video.PageName;
                        
                        //db.Configuration.ProxyCreationEnabled = true;
                        /*
                        var vdo = db.Videos
                            .Where(v => v.VideoId == video.VideoId)
                            .Include(v => v.Tags)
                            .FirstOrDefault();
                        */
                        // remove all tags
                        /*
                        vdo.Tags.Clear(); // break relationship
                        db.SaveChanges();
                        */
                        
                        // remove individual tags
                        foreach (var tg in vdo.Tags.ToList())
                        {
                            // if the updated tags list doesn't contain the database tag, delete it
                            if (!updatedTags.Contains(tg.Name))
                            {
                                vdo.Tags.Remove(tg); // break realation
                                db.SaveChanges();
                            }
                            else
                            {
                                // if the database tags list contains the updated tag, delete it
                                // no need to create relationship or insert to database again, if it is already there
                                updatedTags.Remove(tg.Name);
                            }
                        }
                        /*
                        List<Tag> tagsToAdd = new List<Tag>();
                        foreach (var tag in tags)
                        {
                            Tag newTag = new Tag();
                            newTag.Name = tag;
                            tagsToAdd.Add(newTag);
                        }
                        */
                        // update video
                        db.Entry(vdo).State = EntityState.Modified;
                        db.SaveChanges();

                        List<Tag> updatedTagsList = new List<Tag>();
                        foreach (var tag in updatedTags)
                        {
                            Tag newTag = new Tag();
                            newTag.Name = tag.Trim().ToLower();
                            var tagInDb = db.Tags.FirstOrDefault(t => t.Name.Equals(tag));
                            if (tagInDb == null)
                            {
                                updatedTagsList.Add(newTag);
                            }
                            else
                            {
                                updatedTagsList.Add(tagInDb);
                            }
                        }
                        
                        foreach (var tag in updatedTagsList)
                        {
                            if (tag.TagId.Equals(0))
                            {
                                db.Tags.Add(tag);
                                db.SaveChanges();
                            }

                            db.Tags.Attach(tag);
                            vdo.Tags.Add(tag);
                            db.SaveChanges();
                        }
                        
                        transaction.Commit();
                    }
                    catch(Exception exc)
                    {
                        transaction.Rollback();
                    }
                }
                
                return RedirectToAction("Details/"+video.VideoId);
            }

            return View(video);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Videos/Delete/5
        public ActionResult Delete(int? id)
        {
            //return View();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        [Authorize(Roles = "Administrator")]
        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, FormCollection collection)
        {
            Video video = db.Videos.Find(id);
            db.Videos.Remove(video);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //[AjaxRequestOnly]
        public ActionResult VideoDetails()
        {
            return PartialView("VideoDetails");
        }

        [AjaxRequestOnly]
        public string VideoExists(string EmbedId, string Title)
        {
            EmbedId = (EmbedId == null) ? EmbedId : Uri.UnescapeDataString(EmbedId);
            Title = (Title == null) ? Title : Uri.UnescapeDataString(Title);

            Dictionary<string, object> Video = new Dictionary<string, object>();
            Video["Id"] = EmbedId;
            Video["Title"] = Title;
            Video["Exists"] = VideoHelper.VideoExists(EmbedId);

            return JsonConvert.SerializeObject(Video);
        }
    }
}
