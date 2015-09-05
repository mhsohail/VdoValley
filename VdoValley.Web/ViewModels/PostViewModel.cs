using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VdoValley.Enums;

namespace VdoValley.Web.ViewModels
{
    public class PostViewModel
    {
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public Language Language { get; set; }
        public DateTime DateTime { get; set; }
        public byte[] Thumbnail { get; set; }
        public string ThumbnailUrl { get; set; }
        public int CategoryId { get; set; }
        public int TotalRating { get; set; }
        public int RatingCount { get; set; }
        public string Tags { get; set; }
    }
}