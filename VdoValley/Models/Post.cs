﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VdoValley.Enums;

namespace VdoValley.Models
{
    public class Post
    {
        public int PostId { get; set; }
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

        public virtual Category Category { set; get; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}