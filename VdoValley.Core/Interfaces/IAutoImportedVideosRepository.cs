using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VdoValley.Core.Models;

namespace VdoValley.Core.Interfaces
{
    public interface IAutoImportedVideosRepository
    {
        void Add(AutoImportedVideo Video);
        void Edit(AutoImportedVideo Video);
        void Save(AutoImportedVideo Video);
        void Remove(int VideoId);
        List<AutoImportedVideo> GetVideos();
        AutoImportedVideo FindById(int VideoId);
    }
}
