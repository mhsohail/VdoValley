using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VdoValley.Infrastructure;
using VdoValley.Web.Helpers;
using VdoValley.Core.Models;
using VdoValley.Infrastructure.Repositories;

namespace VdoValley.Web.Areas.API.Controllers
{
    public class VideosController : ApiController
    {
        VideosRepository VideosRepo = new VideosRepository();

        // GET: api/Videos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Videos/5
        public string Get(int id)
        {
            return "value";
        }

        [AcceptVerbs("GET")]
        [ActionName("GetLatestVideos")]
        public HttpResponseMessage GetLatestVideos(int NumberOfVideosToSkip, int NumberOfVideosToTake)
        {
            HttpResponseMessage HttpResponseMessage = new HttpResponseMessage();
            var Videos = VideosRepo.GetLatestVideos(NumberOfVideosToSkip, NumberOfVideosToTake);
            foreach (var Video in Videos)
            {
                Video.ThumbnailURL = VideoHelper.getDailyMotionThumb(Video, VideoHelper.getDailyMotionVideoCode(Video, Video.Url), DailymotionThumbnailSize.thumbnail_large_url);
            }

            HttpResponseMessage = Request.CreateResponse<IEnumerable<Video>>(HttpStatusCode.OK, Videos);
            return HttpResponseMessage;
        }

        // POST: api/Videos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Videos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Videos/5
        public void Delete(int id)
        {
        }
    }
}
