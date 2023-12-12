using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Core.src.Abstractions
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> GetAll();
        User? GetCategoryById(Guid id);
        User CreateCategory(Category category);
        User UpdateCategory(Category category);
        bool DeleteCategory(Guid id);
    }
}