using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VdoValley.Models
{
    public class Category
    {
        public int CategoryId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public virtual ICollection<Video> Videos { set; get; }
    }
}