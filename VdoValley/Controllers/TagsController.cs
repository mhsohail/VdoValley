using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VdoValley.Models;

namespace VdoValley.Controllers
{
    public class TagsController : Controller
    {
        private VdoValleyContext db = new VdoValleyContext();

        // GET: Tags
        public ActionResult Index()
        {
            return View(db.Tags.ToList());
        }

        public string GetTags(List<Tag> tokens)
        {
            string jsonObj = string.Empty;
            try
            {
                // get tag names for each tag and add to list
                List<string> tagNames = new List<string>();
                foreach (Tag token in tokens)
                {
                    tagNames.Add(token.Name);
                }

                // get tag if it's name is in list created above
                IQueryable<Tag> tagss = from tg in db.Tags
                                        where tagNames.Contains(tg.Name)
                                        select tg;
                
                //Tag t = tagss.FirstOrDefault<Tag>();

                jsonObj = JsonConvert.SerializeObject(tagss);

            } catch(Exception exc)
            {

            }
            return jsonObj;
        }

        public string GetTagsFor(int id)
        {
            string tagsStr = string.Empty;
            //List<Tag> tagsModel = new List<Tag>();
            var tags = db.Videos.Where(v => v.VideoId == id).FirstOrDefault().Tags;

            List<Tag> tagsList = new List<Tag>();

            foreach (var tag in tags)
            {
                tagsList.Add(new Tag() { TagId = tag.TagId, Name = tag.Name });
            }

            string json = JsonConvert.SerializeObject(tagsList);
            return json;
        }

        // GET: Tags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // GET: Tags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,VideoId")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Tags.Add(tag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        // GET: Tags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TagId,Name,VideoId")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        // GET: Tags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tag tag = db.Tags.Find(id);
            db.Tags.Remove(tag);
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
