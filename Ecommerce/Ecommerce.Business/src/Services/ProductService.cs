using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
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
        public override Task<ProductReadDTO> CreateOneAsync(ProductCreateDTO createObject)
        {
            return base.CreateOneAsync(createObject);
        }
    }
}
