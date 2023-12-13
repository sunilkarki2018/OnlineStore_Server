using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Core.src.Abstractions
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> GetAllCategories();
        Category? GetCategoryById(Guid id);
        Category CreateCategory(Category category);
        Category UpdateCategory(Category category);
        bool DeleteCategory(Guid id);
    }
}