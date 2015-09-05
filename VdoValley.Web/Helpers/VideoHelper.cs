using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using VdoValley.Core.Models;
using VdoValley.Infrastructure;

namespace VdoValley.Web.Helpers
{
    public class VideoHelper
    {
        public static int GetCommentsCount(int ThreadId)
        {
            int commentsCount = 0;
            string DisqusApiKey = "M02296gbyd8Z6NCaSx7V0oRUAJyj0eiczZLkV4FTKJORcQi1aKlRxNiGMf1R6NPg";
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString("https://disqus.com/api/3.0/threads/set.json?thread=ident:" + ThreadId + "&forum=vdovalley&api_key=" + DisqusApiKey);
                var threads = new
                {
                    response = new[]
                    {
                        new
                        {
                            posts = 0,
                        }
                    }
                };
                threads = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(json, threads);
                commentsCount = threads.response[0].posts;
            }
            return commentsCount;
        }

        public static bool VideoExists(Video video)
        {
            VdoValleyContext db = new VdoValleyContext();
            var vdo = db.Videos.FirstOrDefault(v => v.EmbedId == video.EmbedId && v.EmbedId != null);
            if (vdo == null) { return false; }
            return true;
        }

        public static bool VideoExists(string EmbedId)
        {
            VdoValleyContext db = new VdoValleyContext();
            var vdo = db.Videos.FirstOrDefault(v => v.EmbedId == EmbedId && v.EmbedId != null);
            if (vdo == null) { return false; }
            return true;
        }

        public static string getDailyMotionVideoCode(Video Video, string short_url)
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
            catch (Exception exc)
            {
                code = string.Empty;
            }

            return code;
        }

        public static string getFacebookVideoCode(Video Video, string embed_code)
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
            while (retry);

            return code;
        }

        public static string getDailyMotionThumb(Video Video, string video_code, DailymotionThumbnailSize thumbnail_size)
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

        public static string getFacebookVideoThumbnail(Video Video, string video_code, FacebookThumbnailSize size)
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
            catch (Exception exc)
            {
                thumbnail_path = "http://s4.postimg.org/735fasm5p/loading_spinner_icon.png";
            }

            return thumbnail_path;
        }
    }
}