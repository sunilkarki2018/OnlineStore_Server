using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.src.Entities
{
    public class Image : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public byte[] Data { get; set; }
    }
}
