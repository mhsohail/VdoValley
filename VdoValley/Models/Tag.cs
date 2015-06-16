using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VdoValley.Models
{
    public class Tag
    {
        public int TagId { set; get; }
        public string Name { set; get; }

        [JsonIgnore]
        public virtual ICollection<Video> Videos { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

    }
}