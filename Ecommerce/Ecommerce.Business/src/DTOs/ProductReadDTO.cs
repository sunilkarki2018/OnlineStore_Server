using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.DTOs
{
    public class ProductReadDTO : BaseEntityDTO
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string[] Images { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
    }
    public class ProductCreateDTO
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string[] Images { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CategoryId { get; set; }
    }
     public class ProductUpdateDTO: BaseEntityDTO
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string[] Images { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
    }
}