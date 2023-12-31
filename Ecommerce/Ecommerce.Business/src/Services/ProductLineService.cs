using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Services
{
    public class ProductLineService : BaseService<ProductLine, ProductLineReadDTO, ProductLineCreateDTO, ProductLineUpdateDTO>, IProductLineService
    {
        private readonly IProductLineRepo _productLineRepo;
        private readonly IMapper _mapper;
        public ProductLineService(IProductLineRepo productLineRepo, IMapper mapper) : base(productLineRepo, mapper)
        {
            _productLineRepo = productLineRepo;
            _mapper = mapper;
        }

        public override async Task<bool> UpdateOneAsync(Guid id, ProductLineUpdateDTO updateObject)
        {
            var existingProduct = await _productLineRepo.GetByIdAsync(id);
            if (existingProduct is null)
            {
                throw CustomException.NotFoundException("ProductLine not found");
            }
            _mapper.Map<ProductLineUpdateDTO, ProductLine>(updateObject, existingProduct);
            return await _productLineRepo.UpdateProductLineWithImagesAsync(existingProduct);
        }
    }
}