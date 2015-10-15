using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VdoValley.Core.Models;

namespace VdoValley.Web.ViewModels
{
    public class SidebarPartialViewModel
    {
        public IEnumerable<Video> LatestVideos { get; set; }
    }
}