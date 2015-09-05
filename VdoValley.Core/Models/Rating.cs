using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VdoValley.Core.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public int VideoId { get; set; }
        public int? PostId { get; set; }
        public string ApplicationUserId { set; get; }
        public int Score { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Video Video { get; set; }
        public virtual Post Post { get; set; }
        public virtual ApplicationUser ApplicationUser { set; get; }
    }
}