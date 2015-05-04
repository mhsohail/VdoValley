using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VdoValley.ViewModels
{
    public class VideoViewModel
    {
        public int VideoId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string Tags { get; set; }
        public bool Featured { get; set; }
        public int SelectedCategory { set; get; }
    }
    
}