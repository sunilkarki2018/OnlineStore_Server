using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Status Status { get; set; } = Status.Registered;
        public DateTime OrderDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    public class OrderCreateDTO
    {
        public Status Status { get; set; } = Status.Registered;
        public DateTime OrderDate { get; set; }
        public Guid UserId { get; set; }
    }
    public class OrderUpdateDTO
    {
        public Guid Id { get; set; }
        public Status Status { get; set; } = Status.Registered;
        public DateTime OrderDate { get; set; }
        public Guid UserId { get; set; }
    }
}