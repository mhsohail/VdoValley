using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VdoValley.htmlhelpers
{
    public static class ImageHelper
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, string height, string width)
        {
            var builder = new TagBuilder("img");
            var imgSrc = src.Split('\\').Last();
            var newSrc= System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imgSrc);
            builder.MergeAttribute("class", "img-responsive");
            builder.MergeAttribute("src", newSrc);
            builder.MergeAttribute("alt", altText);
            builder.MergeAttribute("height", height);
            builder.MergeAttribute("width", width);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}