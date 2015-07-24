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
        void Remove(int VideoID);
        IEnumerable<Video> GetVideos();
        Video FindById(int VideoID);
    }
}
