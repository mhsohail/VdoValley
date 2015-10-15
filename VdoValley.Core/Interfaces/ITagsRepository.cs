using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VdoValley.Core.Models;

namespace VdoValley.Interfaces
{
    public interface ITagsRepository
    {
        int Save(Tag Tag);
        void Remove(int TagId);
        List<Tag> GetTags();
        Tag FindById(int TagId);
        List<Tag> GetSuggestedTagsForTitle(string Title, string RootUrl);
    }
}
