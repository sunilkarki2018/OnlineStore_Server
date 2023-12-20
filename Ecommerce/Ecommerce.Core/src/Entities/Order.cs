using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Core.src.Entities
{
    public class Order:BaseEntity
    {
        public OrderStatus OrderStatus { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<OrderItem> orderItems = new List<OrderItem>();
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        Delivered,
        Canceled,
        Registered
    }
}