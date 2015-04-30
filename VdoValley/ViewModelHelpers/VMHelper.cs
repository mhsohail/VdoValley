﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VdoValley.Models;
using VdoValley.ViewModels;

namespace VdoValley.ViewModelHelpers
{
    public static class VMHelper
    {
        public static Video ToDomainVideoModel(VideoViewModel vvm)
        {
            var video = new Video();
            video.VideoId = vvm.VideoId;
            video.Title = vvm.Title;
            video.Url = vvm.Url;
            video.Description = vvm.Description;

            return video;
        }
    }
}