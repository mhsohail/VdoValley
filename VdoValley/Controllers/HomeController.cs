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

    }
}