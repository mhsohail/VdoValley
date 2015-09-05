using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VdoValley.Core.Models
{
    public class VideoType
    {
        [Key]
        public int VideoTypeId { get; set; }
        public VideoTypeEnum VideoTypeName { get; set; }
        
        public virtual ICollection<Video> Videos { get; set; }
    }
}