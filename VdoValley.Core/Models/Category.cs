using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VdoValley.Core.Models;

namespace VdoValley.Core.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public virtual ICollection<Video> Videos { set; get; }
    }
}