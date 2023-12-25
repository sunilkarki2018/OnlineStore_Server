using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.Abstractions
{
    public interface IProductService : IBaseService<Product, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO>
    {
        Task<PaginatedProductReadDTO> GetAllPaginatedProductDTOAsync(GetAllOptions getAllOptions);
    }
}
