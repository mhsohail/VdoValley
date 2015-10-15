using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VdoValley.Core.Models
{
    public class AutoImportedVideo
    {
        public int AutoImportedVideoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailURL { get; set; }
        public string URL { get; set; }
        public string EmbedId { get; set; }
        public int VideoId { get; set; }
        public bool IsShared { get; set; }
    }
}
