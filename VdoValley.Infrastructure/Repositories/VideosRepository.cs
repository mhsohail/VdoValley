using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VdoValley.Interfaces;
using VdoValley.Core.Models;

namespace VdoValley.Infrastructure.Repositories
{
    public class VideosRepository : IVideoRepository
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

        public int Save(Video Video)
        {
            if(Video.VideoId == 0)
            {
                db.Videos.Add(Video);
            }
            else
            {
                db.Entry(Video).State = EntityState.Modified;
            }
            return db.SaveChanges();
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

        public IEnumerable<Video> GetLatestVideos(int NumberOfVideosToSkip, int NumberOfVideosToTake)
        {
            return db.Videos.OrderByDescending(v => v.DateTime).Skip(NumberOfVideosToSkip).Take(NumberOfVideosToTake);
        }
    }
}
