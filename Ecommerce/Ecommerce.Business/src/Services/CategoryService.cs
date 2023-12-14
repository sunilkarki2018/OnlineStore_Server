using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Services
{
    public class CategoryService : BaseService<Category, CategoryReadDTO, CategoryCreateDTO, CategoryUpdateDTO>, ICategoryService
    {
        public CategoryService(ICategoryRepo repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}