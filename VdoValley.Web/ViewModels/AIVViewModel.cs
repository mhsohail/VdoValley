using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VdoValley.Web.ViewModels
{
    public class AIVViewModel
    {
        public int AutoImportedVideoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailURL { get; set; }
        public string URL { get; set; }
        public string EmbedId { get; set; }
        public int VideoId { get; set; }
        public bool IsShared { get; set; }
        public int CategoryId { get; set; }
    }
}