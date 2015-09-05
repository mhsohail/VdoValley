using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace VdoValley.Core.Models
{
    public class Video
    {
        [Key]
        public int VideoId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int? VideoTypeId { get; set; }
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string EmbedCode { get; set; }
        public string EmbedId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        [Display(Name = "Thumbnail URL")]
        public string ThumbnailURL { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string thumbnail_large_url { get; set; }
        public DateTime DateTime { set; get; }
        public int CategoryId { get; set; }
        public bool Featured { get; set; }
        public int TotalRating { get; set; }
        public int RatingCount { get; set; }
        public string PageName { get; set; }

        [JsonIgnore]
        public virtual Category Category { set; get; }
        
        [JsonIgnore]
        public virtual ICollection<Rating> Ratings { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Tag> Tags { get; set; }
        
        [JsonIgnore]
        public virtual VideoType VideoType { get; set; }
    }
}
