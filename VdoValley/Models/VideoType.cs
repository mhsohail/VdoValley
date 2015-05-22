using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VdoValley.Models
{
    public class VideoType
    {
        public int VideoTypeId { get; set; }
        public VideoTypeEnum VideoTypeName { get; set; }
        
        public virtual ICollection<Video> Videos { get; set; }
    }
}