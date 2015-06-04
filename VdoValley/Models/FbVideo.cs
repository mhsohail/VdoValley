using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VdoValley.Models
{
    public class FbVideo
    {
        public string Id {get;set;}
        public string CreatedTime { get; set; }
        public string Description { get; set; }
        public string EmbedHtml { get; set; }
        public bool IsAddedToVdoValley { get; set; }
        public string PageName { get; set; }
    }
}