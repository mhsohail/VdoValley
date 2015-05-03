using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

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
    }
}