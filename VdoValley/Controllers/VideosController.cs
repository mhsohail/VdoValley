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

            VideoViewModel vvm = new VideoViewModel();
            vvm.VideoId = video.VideoId;
            vvm.Url = video.Url;
            vvm.Title = video.Title;
            vvm.SelectedCategory = video.CategoryId;
            vvm.Description = video.Description;
            vvm.DateTime = video.DateTime;
            vvm.Featured = video.Featured;
            TempData["Categories"] = db.Categories.ToList();

            return View(vvm);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VideoId,DateTime,Title,Url,Description,Tags,SelectedCategory")] VideoViewModel vvm)
        {
            Video video = null;
            if (ModelState.IsValid)
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // get the domain model from VM
                        video = ViewModelHelpers.VMHelper.ToDomainVideoModel(vvm);
                        
                        // get tags
                        var updatedTags = vvm.Tags.Split(',').ToList();

                        // load current video from database including tags
                        db.Configuration.ProxyCreationEnabled = false;
                        var vdo = db.Videos.Where(v => v.VideoId.Equals(video.VideoId)).AsNoTracking().Include(v => v.Tags).FirstOrDefault();
                        db.Configuration.ProxyCreationEnabled = true;

                        // remove all tags
                        /*
                        vdo.Tags.Clear(); // break relationship
                        db.SaveChanges();
                        */
                        
                        // remove individual tags
                        foreach (var tg in vdo.Tags.ToList())
                        {
                            // if the database tags list doesn't contain the updated tag, delete it
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
                        db.Entry(video).State = EntityState.Modified;
                        db.SaveChanges();

                        List<Tag> updatedTagsList = new List<Tag>();
                        foreach (var tag in updatedTags)
                        {
                            Tag newTag = new Tag();
                            newTag.Name = tag;
                            updatedTagsList.Add(newTag);
                        }
                        
                        video.Tags = vdo.Tags.ToList();
                        foreach (var tag in updatedTagsList)
                        {
                            db.Tags.Add(tag);
                            db.SaveChanges();
                            //db.Tags.Attach(tag);
                            //video.Tags.Add(tag);
                            //db.SaveChanges();
                        }
                        
                        transaction.Commit();
                    }
                    catch(Exception exc)
                    {
                        transaction.Rollback();
                    }
                }
                
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

        //[AjaxRequestOnly]
        public ActionResult VideoDetails()
        {
            return PartialView("VideoDetails");
        }
    }
}
