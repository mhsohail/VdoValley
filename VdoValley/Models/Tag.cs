using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VdoValley.Models
{
    public class Tag
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int VideoId { set; get; }

        public virtual ICollection<Video> Videos { get; set; }
        public Tag()
        {
            this.Videos = new List<Video>();
        }
    }
}