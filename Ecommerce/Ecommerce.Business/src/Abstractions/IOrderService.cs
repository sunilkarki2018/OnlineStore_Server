using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Abstractions
{
    public interface IOrderService : IBaseService<Order, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>
    {
        Task<OrderReadDTO> CreateOrderAsync(Guid userId,OrderCreateDTO orderCreateDTO);

    }
}
