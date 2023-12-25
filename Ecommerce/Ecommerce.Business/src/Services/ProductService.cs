using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
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
        public ProductService(IProductRepo productRepo, IMapper mapper) : base(productRepo, mapper)
        {
            _productRepo = productRepo;
        }

        public async Task<PaginatedProductReadDTO> GetAllPaginatedProductDTOAsync(GetAllOptions getAllOptions)
        {
            return new PaginatedProductReadDTO()
            {
                Products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductReadDTO>>(await _productRepo.GetAllAsync(getAllOptions)),
                PageCount = Math.Ceiling((decimal)await _productRepo.GetProductsRecordCountAsync(getAllOptions) / getAllOptions.Limit)
            };
        }
    }
}
