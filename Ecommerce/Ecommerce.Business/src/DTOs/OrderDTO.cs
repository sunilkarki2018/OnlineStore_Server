using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.DTOs
{
    public class OrderReadDTO
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Guid UserId { get; set; }
        public string OrderNumber { get; set; }
        public UserReadDTO? User { get; set; }
        public List<OrderItemReadDTO> orderItems { get; set; } = new List<OrderItemReadDTO>();
    }
    public class OrderCreateDTO
    {
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Registered;
        public List<OrderItemCreateDTO> OrderItems { get; set; } = new List<OrderItemCreateDTO>();
    }
    public class OrderUpdateDTO
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}