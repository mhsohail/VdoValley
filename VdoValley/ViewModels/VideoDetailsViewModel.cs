﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VdoValley.Models;

namespace VdoValley.ViewModels
{
    public class VideoDetailsViewModel
    {
        public Video Video { get; set; }
        public double AverageRating { get; set; }
        public Rating VideoScorer { get; set; }
        public int CurrentUsersScore { get; set; }
        public IEnumerable<Video> RelatedVideos { get; set; }
    }
}