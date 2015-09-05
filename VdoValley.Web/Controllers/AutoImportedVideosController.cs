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
using VdoValley.Web.Helpers;

namespace VdoValley.Web.Controllers
{
    public class AutoImportedVideosController : Controller
    {
        private VdoValleyContext db = new VdoValleyContext();

        // GET: AutoImportedVideos
        public ActionResult Index()
        {
            var AIVideos = db.AutoImportedVideos.Where(aiv => !aiv.IsShared).ToList();
            foreach (var AIVideo in AIVideos)
            {
                try
                {
                    var video = db.Videos.SingleOrDefault(v => v.VideoId == AIVideo.VideoId);
                    AIVideo.ThumbnailURL = VideoHelper.getDailyMotionThumb(video, video.EmbedId, DailymotionThumbnailSize.thumbnail_large_url);
                }
                catch(Exception Exc)
                {}
            }

            return View(AIVideos);
        }

        // GET: AutoImportedVideos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoImportedVideo autoImportedVideo = db.AutoImportedVideos.Find(id);
            if (autoImportedVideo == null)
            {
                return HttpNotFound();
            }
            return View(autoImportedVideo);
        }

        // GET: AutoImportedVideos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutoImportedVideos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AutoImportedVideoId,URL,IsShared")] AutoImportedVideo autoImportedVideo)
        {
            if (ModelState.IsValid)
            {
                db.AutoImportedVideos.Add(autoImportedVideo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autoImportedVideo);
        }

        // GET: AutoImportedVideos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoImportedVideo autoImportedVideo = db.AutoImportedVideos.Find(id);
            if (autoImportedVideo == null)
            {
                return HttpNotFound();
            }
            return View(autoImportedVideo);
        }

        // POST: AutoImportedVideos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(AutoImportedVideo AutoImportedVideo, int CategoryId)
        {
            if (ModelState.IsValid)
            {
                db.Entry(AutoImportedVideo).State = EntityState.Modified;
                db.SaveChanges();

                if (CategoryId != null)
                {
                    var Video = db.Videos.SingleOrDefault(v => v.VideoId == AutoImportedVideo.VideoId);
                    Video.CategoryId = CategoryId;
                    db.Entry(Video).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(AutoImportedVideo);
        }

        // GET: AutoImportedVideos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoImportedVideo autoImportedVideo = db.AutoImportedVideos.Find(id);
            if (autoImportedVideo == null)
            {
                return HttpNotFound();
            }
            return View(autoImportedVideo);
        }

        // POST: AutoImportedVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AutoImportedVideo autoImportedVideo = db.AutoImportedVideos.Find(id);
            db.AutoImportedVideos.Remove(autoImportedVideo);
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
    }
}
