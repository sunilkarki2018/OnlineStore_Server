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
        public string OrderNumber { get; set; }
        public User User { get; set; }
        public IEnumerable<OrderItem> orderItems { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        Registered,
        Delivered,
        Canceled,
        Pending
    }
}