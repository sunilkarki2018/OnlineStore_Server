using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebAPI.src.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private DbSet<Category> _categories;
        private DatabaseContext _database;
        public CategoryRepo(DatabaseContext database)
        {
            _categories = database.Categories;
            _database = database;
        }
        public Category CreateCategory(Category category)
        {
            _categories.Add(category);
            _database.SaveChanges();
            return category;
        }

        public bool DeleteCategory(Guid id)
        {
            var category = _categories.Where(x => x.Id == id).FirstOrDefault();
            if (category==null)
            {
                return false;
            }
            _categories.Remove(category);
            var result = _database.SaveChanges();
            return true;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categories;
        }

        public Category? GetCategoryById(Guid id)
        {
            return _categories.Where(u => u.Id == id).FirstOrDefault();
        }

        public Category UpdateCategory(Category category)
        {
            _categories.Update(category);
            _database.SaveChanges();
            return category;
        }
    }
}