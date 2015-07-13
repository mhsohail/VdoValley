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

namespace VdoValley.Web.Controllers
{
    public class VideoosController : Controller
    {
        private VdoValleyContext db = new VdoValleyContext();

        // GET: Videoos
        public ActionResult Index()
        {
            return View(db.Videos.ToList());
        }

        // GET: Videoos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Videoo videoo = db.Videos.Find(id);
            if (videoo == null)
            {
                return HttpNotFound();
            }
            return View(videoo);
        }

        // GET: Videoos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Videoos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VideooId,Title")] Videoo videoo)
        {
            if (ModelState.IsValid)
            {
                db.Videos.Add(videoo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(videoo);
        }

        // GET: Videoos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Videoo videoo = db.Videos.Find(id);
            if (videoo == null)
            {
                return HttpNotFound();
            }
            return View(videoo);
        }

        // POST: Videoos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VideooId,Title")] Videoo videoo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videoo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(videoo);
        }

        // GET: Videoos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Videoo videoo = db.Videos.Find(id);
            if (videoo == null)
            {
                return HttpNotFound();
            }
            return View(videoo);
        }

        // POST: Videoos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Videoo videoo = db.Videos.Find(id);
            db.Videos.Remove(videoo);
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
