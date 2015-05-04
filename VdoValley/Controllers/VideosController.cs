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

namespace VdoValley.Controllers
{
    public class VideosController : Controller
    {
        private VdoValleyContext db = new VdoValleyContext();

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
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }

            //HttpContext.Current.User.Identity.GetUserId();
            ViewBag.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            return View(video);
        }

        // GET: Videos/Create
        public ActionResult Create()
        {
            TempData["Categories"] = db.Categories.ToList();
            VideoViewModel vvm = new VideoViewModel();
            return View(vvm);
        }

        // POST: Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Url,Description,Tags,SelectedCategory")] VideoViewModel vvm)
        {
            if (ModelState.IsValid)
            {
                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        vvm.DateTime = DateTime.Now;
                        var video = ViewModelHelpers.VMHelper.ToDomainVideoModel(vvm);
                        var tags = vvm.Tags.Split(',');
                        List<Tag> tagsToAdd = new List<Tag>();
                        foreach (var tag in tags)
                        {
                            Tag newTag = new Tag();
                            newTag.Name = tag;
                            tagsToAdd.Add(newTag);
                        }

                        db.Videos.Add(video);
                        db.SaveChanges();
                        video.Tags = new List<Tag>();
                        foreach (var tag in tagsToAdd)
                        {
                            db.Tags.Add(tag);
                            db.SaveChanges();
                            db.Tags.Attach(tag);
                            video.Tags.Add(tag);
                            db.SaveChanges();
                        }
                       
                        tran.Commit();
                        return RedirectToAction("Index");
                        
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.ToString());
                        tran.Rollback();
                    }
                }
                return View(vvm);

            }

            return View(vvm);
        }

        // GET: Videos/Edit/5
        public ActionResult Edit(int? id)
        {
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

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Url,Description,thumbnail_large_url")] Video video)
        {
            if (ModelState.IsValid)
            {
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(video);
        }

        // GET: Videos/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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

        [AjaxRequestOnly]
        public ActionResult VideoDetails()
        {
            return PartialView("VideoDetails");
        }
    }
}
