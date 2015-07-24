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
    public class CategoryRepository : ICategoryRepository
    {
        VdoValleyContext db = new VdoValleyContext();

        public void Add(Category Category)
        {
            db.Categories.Add(Category);
            db.SaveChanges();
        }

        public void Edit(Category Category)
        {
            db.Entry(Category).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Remove(int CategoryId)
        {
            Category Category = db.Categories.Find(CategoryId);
            db.Categories.Remove(Category);
            db.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return db.Categories.ToList<Category>();
        }

        public Category FindById(int CategoryId)
        {
            var Category = (from v in db.Categories where v.CategoryId == CategoryId select v).FirstOrDefault();
            return Category;
        }
    }
}
