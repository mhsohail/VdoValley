using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VdoValley.Interfaces;
using VdoValley.Core.Models;

namespace VdoValley.Infrastructure
{
    public class VideoRepository : IVideoRepository
    {
        VdoValleyContext db = new VdoValleyContext();

        public void Add(Video Video)
        {
            db.Videos.Add(Video);
            db.SaveChanges();
        }

        public void Edit(Video Video)
        {
            db.Entry(Video).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Remove(int VideoID)
        {
            Video Video = db.Videos.Find(VideoID);
            db.Videos.Remove(Video);
            db.SaveChanges();
        }

        public List<Video> GetVideos()
        {
            return db.Videos.ToList<Video>();
        }

        public Video FindById(int VideoID)
        {
            var Video = (from v in db.Videos where v.VideoId == VideoID select v).FirstOrDefault();
            return Video;
        }
    }
}
