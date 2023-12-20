using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.src.Entities
{
    public class Avatar:BaseEntity
    {
        public Guid UserId { get; set; }
        public byte[] Data { get; set; }
    }
}
