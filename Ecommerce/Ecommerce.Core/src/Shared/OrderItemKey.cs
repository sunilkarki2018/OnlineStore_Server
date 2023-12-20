using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.src.Shared
{
    public class OrderItemKey
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
    }
}
