using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Core.src.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Status Status { get; set; } = Status.Registered;
        public DateTime OrderDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Delivered,
        Canceled,
        Registered
    }
}