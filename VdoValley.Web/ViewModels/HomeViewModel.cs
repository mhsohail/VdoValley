using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VdoValley.Core.Models;

namespace VdoValley.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Video> LatestVideos { get; set; }
        public IEnumerable<Video> TopRatedVideos { get; set; }
        public Video FeaturedVideo { get; set; }
        public IEnumerable<Video> PoliticsVideos { get; set; }
        public IEnumerable<Video> TalkShowVideos { get; set; }
        public IEnumerable<Video> ShowbizVideos { get; set; }
        public IEnumerable<Video> SportsVideos { get; set; }
        public IEnumerable<Video> FunnyVideos { get; set; }
        public IEnumerable<Video> NewsVideos { get; set; }
    }
}