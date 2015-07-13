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

        public void Add(Videoo Video)
        {
            db.Videos.Add(Video);
            db.SaveChanges();
        }

        public void Edit(Videoo Video)
        {
            db.Entry(Video).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Remove(int VideoID)
        {
            Videoo Video = db.Videos.Find(VideoID);
            db.Videos.Remove(Video);
            db.SaveChanges();
        }

        public List<Videoo> GetVideos()
        {
            return db.Videos.ToList<Videoo>();
        }

        public Videoo FindById(int VideoID)
        {
            var Video = (from v in db.Videos where v.VideooId == VideoID select v).FirstOrDefault();
            return Video;
        }
    }
}
