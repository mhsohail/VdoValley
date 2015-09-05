using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VdoValley.Core.Models
{
    public class Tag
    {
        [Key]
        public int TagId { set; get; }
        public string Name { set; get; }

        [JsonIgnore]
        public virtual ICollection<Video> Videos { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

    }
}