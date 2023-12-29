using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.Services
{
    public class ProductService : BaseService<Product, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO>, IProductService
    {
        protected readonly IProductRepo _productRepo;
        private readonly IProductLineService _productLineService;
        private readonly IProductSizeService _productSizeService;
        private readonly IMapper _mapper;
        public ProductService(IProductRepo productRepo, IMapper mapper, IProductLineService productLineService, IProductSizeService productSizeService) : base(productRepo, mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _productLineService = productLineService;
            _productSizeService = productSizeService;
        }

        public async Task<PaginatedProductReadDTO> GetAllPaginatedProductDTOAsync(GetAllOptions getAllOptions)
        {
            return new PaginatedProductReadDTO()
            {
                Products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductReadDTO>>(await _productRepo.GetAllAsync(getAllOptions)),
                PageCount = Math.Ceiling((decimal)await _productRepo.GetProductsRecordCountAsync(getAllOptions) / getAllOptions.Limit)
            };
        }

        public override async Task<ProductReadDTO> CreateOneAsync(ProductCreateDTO createObject)
        {
            var createdProduct = await _productRepo.CreateOneAsync(_mapper.Map<ProductCreateDTO, Product>(createObject));
            var productReadDTO = _mapper.Map<Product, ProductReadDTO>(createdProduct);
            productReadDTO.ProductLine = await _productLineService.GetByIdAsync(productReadDTO.ProductLineId);
            
            productReadDTO.ProductSize = await _productSizeService.GetByIdAsync(productReadDTO.ProductSizeId.Value);
            return productReadDTO;
        }
    }
}
