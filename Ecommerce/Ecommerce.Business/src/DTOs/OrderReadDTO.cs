using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.DTOs
{
    public class OrderReadDTO
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
         public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<OrderItemReadDTO> orderItems { get; set; }=new List<OrderItemReadDTO>();
    }
    public class OrderCreateDTO
    {
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Registered;
        public DateTime OrderDate { get; set; }
        public Guid UserId { get; set; }
    }
    public class OrderUpdateDTO
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid UserId { get; set; }
    }
}