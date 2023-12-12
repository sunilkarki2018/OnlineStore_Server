using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Core.src.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public int Rating { get; set; } // Assuming Rating is an integer within the range [1, 5]
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}