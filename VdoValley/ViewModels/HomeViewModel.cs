using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VdoValley.Models;

namespace VdoValley.ViewModels
{
    public class HomeViewModel
    {
        public List<Video> AllVideos { get; set; }
        public List<Video> TopRatedVideos { get; set; }
        public Video FeaturedVideo { get; set; }
    }
}