using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VdoValley.Core.Models;

namespace VdoValley.Interfaces
{
    public interface IVideoRepository
    {
        void Add(Video Video);
        void Edit(Video Video);
        int Save(Video Video);
        void Remove(int VideoID);
        List<Video> GetVideos();
        Video FindById(int VideoID);
    }
}
