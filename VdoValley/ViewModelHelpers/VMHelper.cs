using System;
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
            video.CategoryId = vvm.SelectedCategory;
            video.Title = (vvm.Title == null) ? vvm.Title : Uri.UnescapeDataString(vvm.Title);
            video.Url = (vvm.Url == null) ? vvm.Url : Uri.UnescapeDataString(vvm.Url);
            video.Description = (vvm.Description == null) ? vvm.Description : Uri.UnescapeDataString(vvm.Description);
            video.Featured = vvm.Featured;
            video.DateTime = vvm.DateTime;
            video.EmbedCode = vvm.EmbedCode;
            video.EmbedId = vvm.EmbedId;
            video.VideoTypeId = vvm.VideoTypeId;
            video.PageName = vvm.PageName;
            video.ThumbnailURL = vvm.ThumbnailURL;
            
            return video;
        }
    }
}