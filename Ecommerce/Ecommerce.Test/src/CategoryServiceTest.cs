using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Services;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Moq;

namespace Ecommerce.Test.src
{
    public class CategoryServiceTest
    {

        private readonly Mock<ICategoryRepo> _mockRepo;
        private static IMapper _mapper;

        public CategoryServiceTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(m =>
                {
                    m.AddProfile(new MapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async void GetAllCategoriesAsync_ShouldInvokeRepoCategoriesMethod()
        {
            var repo = new Mock<ICategoryRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new CategoryService(repo.Object, _mapper);
            GetAllOptions options = new GetAllOptions() { Limit = 5, Offset = 0 };

            await categoryService.GetAllAsync(options);

            repo.Verify(repo => repo.GetAllAsync(options), Times.Once);
        }

        [Theory]
        [ClassData(typeof(GetAllCategoriesData))]
        public async void GetAllCategoriesAsync_ShouldReturnValidResponse(IEnumerable<Category> repoResponse, IEnumerable<CategoryReadDTO> expected)
        {
            var repo = new Mock<ICategoryRepo>();
            GetAllOptions options = new GetAllOptions() { Limit = 5, Offset = 0 };
            repo.Setup(repo => repo.GetAllAsync(options)).ReturnsAsync(repoResponse);
            var categoryService = new CategoryService(repo.Object, _mapper);

            var response = await categoryService.GetAllAsync(options);

            Assert.Equivalent(expected, response);
        }

        public class GetAllCategoriesData : TheoryData<IEnumerable<Category>, IEnumerable<CategoryReadDTO>>
        {
            public GetAllCategoriesData()
            {
                Category category1 = new Category() { Id = Guid.NewGuid(), Name = "Shirt", Image = "image1" };
                Category category2 = new Category() { Id = Guid.NewGuid(), Name = "Pants", Image = "image2" };
                Category category3 = new Category() { Id = Guid.NewGuid(), Name = "Jacket", Image = "image3" };
                IEnumerable<Category> categories = new List<Category>() { category1, category2, category3 };

                Add(categories, _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryReadDTO>>(categories));
            }
        }

        [Fact]
        public async void GetCategoryByIdAsync_ShouldInvokeRepoCategoryMethod()
        {
            var id = Guid.NewGuid();
            var repo = new Mock<ICategoryRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new CategoryService(repo.Object, _mapper);

            await categoryService.GetByIdAsync(id);

            repo.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        }

        [Theory]
        [ClassData(typeof(GetCategoryByIdData))]
        public async void GetCategoryByIdAsync_ShouldReturnValidResponse(Category category, CategoryReadDTO categoryReadDTO, Type exceptionType)
        {
            var repo = new Mock<ICategoryRepo>();
            repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category);
            var categoryService = new CategoryService(repo.Object, _mapper);

            var response = await categoryService.GetByIdAsync(It.IsAny<Guid>());

            Assert.Equivalent(categoryReadDTO, response);
        }

        public class GetCategoryByIdData : TheoryData<Category?, CategoryReadDTO?, Type?>
        {
            public GetCategoryByIdData()
            {
                Category category = new Category() { Name = "HnM", Image = "image1" };
                CategoryReadDTO categoryReadDTO = _mapper.Map<Category, CategoryReadDTO>(category);
                Add(category, categoryReadDTO, null);
            }
        }

        [Fact]
        public async void CreateOneAsync_ShouldInvokeRepoMethod()
        {
            var repo = new Mock<ICategoryRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new CategoryService(repo.Object, _mapper);
            CategoryCreateDTO dto = new CategoryCreateDTO() { Name = "Hnm", Image = "image1" };

            await categoryService.CreateOneAsync(dto);

            repo.Verify(repo => repo.CreateOneAsync(It.IsAny<Category>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(CreateCategoryData))]
        public async void CreateOneAsync_ShouldReturnValidResponse(Category repoResponse, CategoryReadDTO expected, Type? exceptionType)
        {
            var categoryRepo = new Mock<ICategoryRepo>();
            categoryRepo.Setup(repo => repo.CreateOneAsync(It.IsAny<Category>())).ReturnsAsync(repoResponse);
            var categoryService = new CategoryService(categoryRepo.Object, _mapper);
            CategoryCreateDTO categoryCreateDTO = new CategoryCreateDTO() { Name = "HnM", Image = "image1" };

            if (exceptionType is not null)
            {
                await Assert.ThrowsAsync(exceptionType, () => categoryService.CreateOneAsync(categoryCreateDTO));
            }
            else
            {
                var response = await categoryService.CreateOneAsync(categoryCreateDTO);
                Assert.Equivalent(expected, response);
            }
        }
        public class CreateCategoryData : TheoryData<Category?, CategoryReadDTO?, Type?>
        {
            public CreateCategoryData()
            {
                Category category = new Category() { Name = "HnM", Image = "image1" };
                CategoryReadDTO categoryReadDTO = _mapper.Map<Category, CategoryReadDTO>(category);
                Add(category, categoryReadDTO, null);
            }
        }

        [Fact]
        public async void DeleteCategoryByIdAsync_ShouldInvokeRepoCategoryMethod()
        {
            var id = Guid.NewGuid();
            Category category = new Category() { Id = id, Name = "HnM", Image = "image1" };
            var categoryRepo = new Mock<ICategoryRepo>();
            var mapper = new Mock<IMapper>();
            categoryRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category);
            categoryRepo.Setup(repo => repo.DeleteOneAsync(It.IsAny<Category>())).ReturnsAsync(true);

            var categoryService = new CategoryService(categoryRepo.Object, _mapper);

            await categoryService.DeleteOneAsync(id);

            categoryRepo.Verify(repo => repo.DeleteOneAsync(category), Times.Once);
        }

        [Theory]
        [ClassData(typeof(DeleteCategoryData))]
        public async void DeleteCategoryAsync_ShouldReturnValidResponse(Guid id, bool? expected)
        {
            Category category = new Category() { Id = id, Name = "HnM", Image = "image1" };
            var categoryRepo = new Mock<ICategoryRepo>();
            var mapper = new Mock<IMapper>();
            categoryRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category);
            categoryRepo.Setup(repo => repo.DeleteOneAsync(It.IsAny<Category>())).ReturnsAsync(true);
            var categoryService = new CategoryService(categoryRepo.Object, _mapper);

            var response = await categoryService.DeleteOneAsync(It.IsAny<Guid>());

            Assert.Equivalent(expected, response);
        }
        public class DeleteCategoryData : TheoryData<Guid, bool>
        {
            public DeleteCategoryData()
            {
                var id = Guid.NewGuid();
                Add(id, true);
            }
        }
        [Fact]
        public async void UpdateCategoryByIdAsync_ShouldInvokeRepoCategoryMethod()
        {
            var id = Guid.NewGuid();
            var categoryRepo = new Mock<ICategoryRepo>();
            var mapper = new Mock<IMapper>();
            var categoryService = new CategoryService(categoryRepo.Object, _mapper);
            CategoryUpdateDTO categoryUpdateDTO = new CategoryUpdateDTO() { Name = "Hnm", Image = "image1" };

            await categoryService.UpdateOneAsync(id, categoryUpdateDTO);

            categoryRepo.Verify(repo => repo.UpdateOneAsync(It.IsAny<Category>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(UpdateCategoryData))]
        public async void UpdateOneAsync_ShouldReturnValidResponse(CategoryUpdateDTO categoryUpdateDTO, bool expected, Type? exceptionType)
        {
            var categoryRepo = new Mock<ICategoryRepo>();
            categoryRepo.Setup(repo => repo.UpdateOneAsync(It.IsAny<Category>())).ReturnsAsync(true);
            var categoryService = new CategoryService(categoryRepo.Object, _mapper);

            var response = await categoryService.UpdateOneAsync(Guid.NewGuid(), categoryUpdateDTO);
            Assert.Equivalent(expected, response);

        }
        public class UpdateCategoryData : TheoryData<CategoryUpdateDTO, bool, Type?>
        {
            public UpdateCategoryData()
            {
                CategoryUpdateDTO categoryUpdateDTO = new CategoryUpdateDTO() { Name = "HnM", Image = "image1" };
                Add(categoryUpdateDTO, true, null);
            }
        }
    }
}