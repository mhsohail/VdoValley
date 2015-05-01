using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VdoValley.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public string ApplicationUserId { set; get; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
        public Video Video { get; set; }
        public ApplicationUser ApplicationUser { set; get; }
    }
}