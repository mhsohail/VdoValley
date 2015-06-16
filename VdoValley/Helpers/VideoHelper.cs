using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using VdoValley.Models;

namespace VdoValley.Helpers
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
    }
}