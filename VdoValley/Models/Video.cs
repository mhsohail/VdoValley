using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
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
        public int? VideoTypeId { get; set; }
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string EmbedCode { get; set; }
        public string EmbedId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        [Display(Name = "Thumbnail URL")]
        public string ThumbnailURL { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string thumbnail_large_url { get; set; }
        public DateTime DateTime { set; get; }
        public int CategoryId { get; set; }
        public bool Featured { get; set; }
        public int TotalRating { get; set; }
        public int RatingCount { get; set; }
        public string PageName { get; set; }

        public virtual Category Category { set; get; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual VideoType VideoType { get; set; }
        
        public string getDailyMotionVideoCode(string short_url)
        {
            string code = string.Empty;
            string pattern = @"(http://dai.ly/)(\S*)";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = null;

            try
            {
                matches = rgx.Matches(short_url);
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        code = matches[0].Groups[2].Value;
                    }
                }
            }
            catch(Exception exc)
            {
                code = string.Empty;
            }
            
            return code;
        }

        public string getFacebookVideoCode(string embed_code)
        {
            //<div id="fb-root"></div><div class="fb-video" data-allowfullscreen="true" data-href="/KaroMAZAcom/videos/vb.1415391255370815/1625483954361543/?type=1"><div class="fb-xfbml-parse-ignore"><blockquote cite="/KaroMAZAcom/videos/1625483954361543/"><a href="/KaroMAZAcom/videos/1625483954361543/"></a><p>Kamal Kar diya Isny Tou</p>Posted by <a href="https://www.facebook.com/KaroMAZAcom">Karo MAZA</a> on Saturday, May 16, 2015</blockquote></div></div>
            //string pattern = ;
            string code = string.Empty;
            //string pattern = "data-href=\\\"/[A-Za-z]+/videos/vb.[0-9]+/[0-9]+/?type=1";

            List<string> patterns = new List<string>();
            patterns.Add("data-href=\\\"/[A-Za-z0-9.]+/videos/(vb.[0-9]+/)?([0-9]+)/(\\?type=1)?");
            patterns.Add("data-href=\\\"https://www.(facebook).com/video.php\\?v=([0-9]+)");
            patterns.Add("<iframe src=\\\"https://www.(facebook).com/video/embed\\?video_id=([0-9]+)\"");

            int i = 0;
            bool retry = false;
            do
            {
                try
                {
                    Regex rgx = new Regex(patterns[i], RegexOptions.IgnoreCase);
                    MatchCollection matches = rgx.Matches(embed_code);
                    code = matches[0].Groups[2].Value;
                    retry = false;
                }
                catch (ArgumentOutOfRangeException exc)
                {
                    if (i != patterns.Count - 1)
                    {
                        i++;
                        retry = true;
                    }
                    else
                    {
                        code = string.Empty;
                        break;
                    }
                }
                catch (Exception)
                {
                    code = string.Empty;
                }
            }
            while(retry);

            return code;
        }

        public string getDailyMotionThumb(string video_code, DailymotionThumbnailSize thumbnail_size)
        {
            string size = thumbnail_size.ToString();
            string json = string.Empty;
            Video vdo = new Video();
            try
            {
                using (var client = new WebClient())
                {
                    json = client.DownloadString("https://api.dailymotion.com/video/" + video_code + "?fields=" + size);
                }
                vdo = JsonConvert.DeserializeObject<Video>(json);
            }
            catch (Exception exc)
            {
                vdo.thumbnail_large_url = "http://s4.postimg.org/735fasm5p/loading_spinner_icon.png";
            }
            
            return vdo.thumbnail_large_url;
        }

        public string getFacebookVideoThumbnail(string video_code, FacebookThumbnailSize size)
        {
            string thumbnail_path = string.Empty;
            string json = string.Empty;
            try
            {
                using (var client = new WebClient())
                {
                    json = client.DownloadString("https://graph.facebook.com/" + video_code);
                }

                var fbVideo = new
                {
                    description = string.Empty,
                    format = new[]
                {
                    new
                    {
                        picture = string.Empty
                    }
                }
                };

                var vdo = JsonConvert.DeserializeAnonymousType(json, fbVideo);

                if (size.Equals(FacebookThumbnailSize.SMALL))
                {
                    thumbnail_path = vdo.format[0].picture;
                }
                else
                {
                    thumbnail_path = vdo.format[1].picture;
                }
            }
            catch(Exception exc)
            {
                thumbnail_path = "http://s4.postimg.org/735fasm5p/loading_spinner_icon.png";
            }
            
            return thumbnail_path;
        }
    }

}