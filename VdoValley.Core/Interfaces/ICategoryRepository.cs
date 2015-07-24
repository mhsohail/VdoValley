using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VdoValley.Core.Models;

namespace VdoValley.Interfaces
{
    public interface ICategoryRepository
    {
        void Add(Category Category);
        void Edit(Category Category);
        void Remove(int CategoryId);
        List<Category> GetCategories();
        Category FindById(int CategoryId);
    }
}
