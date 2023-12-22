using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Abstractions
{
    public interface IProductLineService: IBaseService<ProductLine, ProductLineReadDTO, ProductLineCreateDTO, ProductLineUpdateDTO>
    {
        
    }
}