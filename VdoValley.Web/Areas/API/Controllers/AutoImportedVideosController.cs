using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using VdoValley.Core.Models;
using VdoValley.Infrastructure;
using VdoValley.Infrastructure.Repositories;
using VdoValley.Web.Helpers;
using VdoValley.Web.ViewModelHelpers;
using VdoValley.Web.ViewModels;

namespace VdoValley.Web.Areas.API.Controllers
{
    public class AutoImportedVideosController : ApiController
    {
        AutoImportedVideosRepository AIVRepo;
        VideosRepository VideosRepo;

        public AutoImportedVideosController()
        {
            AIVRepo = new AutoImportedVideosRepository();
            VideosRepo = new VideosRepository();
        }

        // GET: api/AutoImportedVideos/Index
        [AcceptVerbs("GET")]
        [ActionName("Index")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [AcceptVerbs("GET")]
        [ActionName("Details")]
        // GET: api/AutoImportedVideos/Details/5
        public string Get(int id)
        {
            return "value";
        }

        [AcceptVerbs("POST")]
        [ActionName("Create")]
        // POST: api/AutoImportedVideos/Create
        public void Post([FromBody]string value)
        {
        }

        [AcceptVerbs("POST")]
        [ActionName("ImportZemTvVideos")]
        // POST: api/AutoImportedVideos/ImportZemTvVideos
        public HttpResponseMessage ImportZemTvVideos(int page)
        {
            MatchCollection aMatchCollection = null;
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                string htmlCode = client.DownloadString("http://www.zemtv.com/category/user-submitted/page/" + page + "/");
                string Pattern = "(?<=<div class=\"thumbnail\">[\\r\\n\\s]*<a href=\")http://www.zemtv.com/[0-9]{4}/[0-9]{2}/[0-9]{2}/[a-zA-Z_0-9-]+";
                Regex aRegex = new Regex(Pattern, RegexOptions.IgnoreCase);
                aMatchCollection = aRegex.Matches(htmlCode);
                foreach (Match aMatch in aMatchCollection.Cast<Match>().Reverse())
                {
                    using (WebClient client2 = new WebClient()) // WebClient class inherits IDisposable
                    {
                        htmlCode = client.DownloadString(aMatch.Value);
                        Pattern = "<iframe.+src=\"http://www.dailymotion.com/embed/video/(\\w+)?.+\"></iframe>";
                        aRegex = new Regex(Pattern, RegexOptions.IgnoreCase);
                        Match aMatch2 = aRegex.Match(htmlCode);
                        if (aMatch2.Success)
                        {
                            var VideoCode = aMatch2.Groups[1].Value;
                            if (VideoHelper.VideoExists(VideoCode)) continue;
                            //"https://api.dailymotion.com/video/" + videoCode + "?fields=id,title,description"
                            string requestUrl = string.Format("https://api.dailymotion.com/video/" + VideoCode + "?fields=id,title,description");
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                            request.Method = "GET";
                            request.Accept = "application/json;charset=UTF-8";

                            try
                            {
                                var httpResponse = (HttpWebResponse)request.GetResponse();
                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                {
                                    var responstText = streamReader.ReadToEnd();
                                    //Receipt = serializer.Deserialize<Receipt>(responstText);
                                    var DMVideo = new
                                    {
                                        Id = string.Empty,
                                        Title = string.Empty,
                                        Description = string.Empty
                                    };
                                    
                                    DMVideo = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(responstText, DMVideo);
                                    var SuggestedTags = new TagsRepository().GetSuggestedTagsForTitle(DMVideo.Title, string.Format("{0}://{1}{2}", Request.RequestUri.Scheme, Request.RequestUri.Authority, Url.Content("~")));
                                    VdoValley.Web.ViewModels.VideoViewModel vvm = new VdoValley.Web.ViewModels.VideoViewModel();

                                    var SuggestedTagsStr = string.Empty;
                                    if (SuggestedTags.Count > 0) SuggestedTagsStr = SuggestedTags[0].Name;
                                    foreach (var SuggestedTag in SuggestedTags.Skip(1))
                                    {
                                        SuggestedTagsStr += "," + SuggestedTag.Name;
                                    }

                                    vvm.Tags = SuggestedTagsStr;
                                    vvm.Title = DMVideo.Title;
                                    vvm.EmbedId = DMVideo.Id;
                                    vvm.Description = DMVideo.Description;
                                    vvm.VideoTypeId = 1;
                                    vvm.Url = "http://dai.ly/" + DMVideo.Id;
                                    vvm.SelectedCategory = 1;
                                    vvm.Featured = false;
                                    vvm.IsJsonRequest = true;
                                    vvm.IsAutoImported = true;

                                    var vvv = new VdoValley.Web.Controllers.VideosController().Create(vvm);
                                }
                            }
                            catch (WebException WebExc)
                            {
                                var Response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, WebExc.Message);
                                return Response;
                            }
                            catch (Exception Exc)
                            {
                                var Response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Exc.Message);
                                return Response;
                            }
                        }
                    }
                }
            }

            return Request.CreateResponse<string>(HttpStatusCode.OK, "Video saved");
        }

        [AcceptVerbs("PUT")]
        [ActionName("Edit")]
        // PUT: api/AutoImportedVideos/Edit/5
        public HttpResponseMessage Put(AIVViewModel AIVViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var AIVideo = VMHelper.ToDomainAIVideoModel(AIVViewModel);
                    AIVRepo.Save(AIVideo);

                    if (AIVViewModel.CategoryId != 0)
                    {
                        var Video = VideosRepo.FindById(AIVViewModel.VideoId);
                        Video.CategoryId = AIVViewModel.CategoryId;
                        VideosRepo.Save(Video);
                    }
                    return Request.CreateResponse<string>(HttpStatusCode.OK, "Video saved");
                }
                catch(Exception exc)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc.Message);
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid model");
            }
        }
        
        [AcceptVerbs("DELETE")]
        [ActionName("Delete")]
        // DELETE: api/AutoImportedVideos/Delete/5
        public void Delete(int id)
        {
        }
    }
}
