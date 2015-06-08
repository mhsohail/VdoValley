using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VdoValley.Models;

namespace VdoValley.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Video> AllVideos { get; set; }
        public IEnumerable<Video> TopRatedVideos { get; set; }
        public Video FeaturedVideo { get; set; }
        public IEnumerable<Video> PoliticsVideos { get; set; }
        public IEnumerable<Video> TalkShowVideos { get; set; }
        public IEnumerable<Video> FashionVideos { get; set; }
        public IEnumerable<Video> SportsVideos { get; set; }
        public IEnumerable<Video> FunnyVideos { get; set; }
    }
}