using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Abstractions
{
    public interface IProductSizeService: IBaseService<ProductSize, ProductSizeReadDTO, ProductSizeCreateDTO, ProductSizeUpdateDTO>
    {
        
    }
}