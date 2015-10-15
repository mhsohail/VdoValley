using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VdoValley.Interfaces;
using VdoValley.Core.Models;
using VdoValley.Core.Interfaces;

namespace VdoValley.Infrastructure.Repositories
{
    public class AutoImportedVideosRepository : IAutoImportedVideosRepository
    {
        VdoValleyContext db = new VdoValleyContext();

        public void Add(AutoImportedVideo Video)
        {
            db.AutoImportedVideos.Add(Video);
            db.SaveChanges();
        }

        public void Edit(AutoImportedVideo Video)
        {
            db.Entry(Video).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Save(AutoImportedVideo Video)
        {
            if(Video.VideoId == 0 || Video.VideoId == null)
            {
                db.AutoImportedVideos.Add(Video);
            }
            else
            {
                db.Entry(Video).State = EntityState.Modified;
            }
            db.SaveChanges();
        }

        public void Remove(int VideoId)
        {
            AutoImportedVideo Video = db.AutoImportedVideos.Find(VideoId);
            db.AutoImportedVideos.Remove(Video);
            db.SaveChanges();
        }

        public List<AutoImportedVideo> GetVideos()
        {
            return db.AutoImportedVideos.ToList<AutoImportedVideo>();
        }

        public AutoImportedVideo FindById(int VideoId)
        {
            var Video = (from v in db.AutoImportedVideos where v.AutoImportedVideoId == VideoId select v).SingleOrDefault();
            return Video;
        }
    }
}
