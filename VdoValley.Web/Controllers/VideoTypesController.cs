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

namespace VdoValley.Controllers
{
    public class VideoTypesController : Controller
    {
        private VdoValleyContext db = new VdoValleyContext();

        // GET: VideoTypes
        public ActionResult Index()
        {
            return View(db.VideoTypes.ToList());
        }

        // GET: VideoTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoType videoType = db.VideoTypes.Find(id);
            if (videoType == null)
            {
                return HttpNotFound();
            }
            return View(videoType);
        }

        // GET: VideoTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VideoTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VideoTypeeId,VideoTypeName")] VideoType videoType)
        {
            if (ModelState.IsValid)
            {
                db.VideoTypes.Add(videoType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(videoType);
        }

        // GET: VideoTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoType videoType = db.VideoTypes.Find(id);
            if (videoType == null)
            {
                return HttpNotFound();
            }
            return View(videoType);
        }

        // POST: VideoTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VideoTypeId,VideoTypeName")] VideoType videoType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videoType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(videoType);
        }

        // GET: VideoTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoType videoType = db.VideoTypes.Find(id);
            if (videoType == null)
            {
                return HttpNotFound();
            }
            return View(videoType);
        }

        // POST: VideoTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VideoType videoType = db.VideoTypes.Find(id);
            db.VideoTypes.Remove(videoType);
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
