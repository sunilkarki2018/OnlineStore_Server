using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.DTOs
{
    public class ReviewReadDTO
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProductName { get; set; }
    }
    public class ReviewCreateDTO
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }

    }
}