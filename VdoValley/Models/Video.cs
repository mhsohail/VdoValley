using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace VdoValley.Models
{
    public class Video
    {
        [Key]
        public int VideoId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string thumbnail_large_url { get; set; }
        
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }


        public string getDailyMotionVideoCode(string short_url)
        {
            string code = string.Empty;
            string pattern = @"(http://dai.ly/)(\S*)";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(short_url);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    code = match.Value;
                }
            }
            
            return matches[0].Groups[2].Value;
        }
    }
}