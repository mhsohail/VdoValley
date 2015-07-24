using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VdoValley.Web.ViewModels
{
    public class VideoViewModel
    {
        public int VideoId { get; set; }
        [Required]
        public string Title { get; set; }
        public int VideoTypeId { get; set; }
        [Display(Name="Short URL")]
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        [Display(Name = "Thumbnail URL")]
        public string ThumbnailURL { get; set; }
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string EmbedCode { get; set; }
        public string EmbedId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        [Display(Name="Tags (Comma Separated)")]
        public string Tags { get; set; }
        public bool Featured { get; set; }
        [Display(Name="Category")]
        public int SelectedCategory { set; get; }
        public string PageName { set; get; }
        public bool IsJsonRequest { get; set; }
    }
}