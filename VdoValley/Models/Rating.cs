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
        public int Score { get; set; }
        public DateTime Date { get; set; }
        public Video Video { get; set; }
    }
}