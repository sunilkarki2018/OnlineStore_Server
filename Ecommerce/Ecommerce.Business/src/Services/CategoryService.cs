using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepo _categoryRepo;
        private IMapper _mapper;

        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        public CategoryReadDTO CreateCategory(CategoryCreateDTO categoryCreateDTO)
        {
            var result = _categoryRepo.CreateCategory(_mapper.Map<CategoryCreateDTO, Category>(categoryCreateDTO));
            return _mapper.Map<Category, CategoryReadDTO>(result);
        }

        public bool DeleteUser(Guid id)
        {
            return _categoryRepo.DeleteCategory(id);
        }

        public IEnumerable<CategoryReadDTO> GetAllCategories()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryReadDTO>>(_categoryRepo.GetAllCategories());
        }

        public CategoryReadDTO? GetCategoryById(Guid id)
        {
            var result = _categoryRepo.GetCategoryById(id);
            if (result is null)
            {
                return null;
            }
            return _mapper.Map<Category, CategoryReadDTO>(result);
        }

        public CategoryReadDTO? UpdateCategory(CategoryUpdateDTO categoryUpdateDTO)
        {
            var existingCategory = _categoryRepo.GetCategoryById(categoryUpdateDTO.Id);
            if (existingCategory == null)
            {
                return null;
            }
            _mapper.Map<CategoryUpdateDTO, Category>(categoryUpdateDTO, existingCategory);
            return _mapper.Map<Category, CategoryReadDTO>(_categoryRepo.UpdateCategory(existingCategory));
        }
    }
}