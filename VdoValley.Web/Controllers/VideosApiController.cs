using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VdoValley.Core.Models;
using VdoValley.Infrastructure;
using VdoValley.Infrastructure.Repositories;
using VdoValley.Web.Helpers;

namespace VdoValley.Web.Controllers
{
    public class VideosApiController : ApiController
    {
        VideosRepository VideosRepo = new VideosRepository();

        // GET: api/VideosApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public HttpResponseMessage GetLatestVideos(int NumberOfVideosToSkip, int NumberOfVideosToTake)
        {
            HttpResponseMessage HttpResponseMessage = new HttpResponseMessage();
            var Videos = VideosRepo.GetLatestVideos(NumberOfVideosToSkip, NumberOfVideosToTake);
            foreach(var Video in Videos)
            {
                Video.ThumbnailURL = VideoHelper.getDailyMotionThumb(Video, VideoHelper.getDailyMotionVideoCode(Video, Video.Url), DailymotionThumbnailSize.thumbnail_large_url);
            }
            
            HttpResponseMessage = Request.CreateResponse<IEnumerable<Video>>(HttpStatusCode.OK, Videos);
            return HttpResponseMessage;
        }

        // GET: api/VideosApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/VideosApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/VideosApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/VideosApi/5
        public void Delete(int id)
        {
        }
    }
}
