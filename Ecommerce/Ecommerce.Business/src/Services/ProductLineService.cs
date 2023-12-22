using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Services
{
    public class ProductLineService : BaseService<ProductLine, ProductLineReadDTO, ProductLineCreateDTO, ProductLineUpdateDTO>, IProductLineService
    {
        public ProductLineService(IProductLineRepo repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}