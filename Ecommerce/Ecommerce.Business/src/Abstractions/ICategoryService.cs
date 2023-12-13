using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Business.src.DTOs;

namespace Ecommerce.Business.src.Abstractions
{
    public interface ICategoryService
    {
        IEnumerable<CategoryReadDTO> GetAllCategories();
        CategoryReadDTO? GetCategoryById(Guid id);
        CategoryReadDTO CreateCategory(CategoryCreateDTO categoryCreateDTO);
        CategoryReadDTO? UpdateCategory(CategoryUpdateDTO categoryUpdateDTO);
        bool DeleteUser(Guid id);
    }
}