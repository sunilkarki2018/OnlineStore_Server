using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Core.src.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}